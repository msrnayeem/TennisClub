﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TennisWeb.CF;
using TennisWeb.Models;
using Match = TennisWeb.CF.Match;
using System.Data.Entity;
using System.IO;

namespace TennisWeb.Services
{
    public class MatchService
    {
        public static List<CF.Match> GetMatches()
        {
            using (var db = new TennisContext())
            {
                return db.Matches.Include("Slot").Include("CoachInfo").ToList();
            }
        }

        public static string AddMatch(CF.Match match)
        {
            using (var db = new TennisContext())
            {
                try
                {
                    // Check if a match with the same slot and time already exists
                    bool matchExists = db.Matches.Any(m => m.SlotId == match.SlotId && m.Time == match.Time);

                    if (!matchExists)
                    {
                        // If no match with the same slot and time exists, add the new match
                        db.Matches.Add(match);
                        db.SaveChanges();

                        return "Match added successfully!";
                    }

                    // If a match with the same slot and time already exists, return an error message
                    return "A match with the same slot and time already exists. Match not added.";
                }
                catch (Exception ex)
                {
                    // If there is a database error, return the specific error message
                    return $"Error: {ex.Message}";
                }
            }
        }


        public static Match GetMatchInformation(int id)
        {
            using(var db = new TennisContext())
            {
                return db.Matches
                    .Include(m => m.Slot)
                 .Include(m => m.CoachInfo)
                 .Include(m => m.MatchPlayers.Select(mp => mp.PlayerInfo)) // Include MatchPlayers and their related PlayerInfo entities
                 .Where(m => m.Id == id)
                 .SingleOrDefault();
            }
        }


        //free slots
        public static List<int> GetBookedSlots(DateTime date)
        {
            using (var db = new TennisContext())
            {
                return db.Matches.Where(s => s.Time == date).Select(x => x.SlotId).ToList();
            }
        }

        public static List<Object> GetFreeSlots(DateTime date)
        {
            using (var db = new TennisContext())
            {
                //  var booked = Services.MatchService.GetBookedSlots(DateTime.Now);
                var bookedSlotIds = Services.MatchService.GetBookedSlots(date);
                var allSlots = db.Slots.ToList();

                var freeSlots = allSlots
                .Where(slot => !bookedSlotIds.Contains(slot.Id))
                .Select(slot => new
                {
                    slot.Id,
                    slot.Name,
                    slot.Start,
                    slot.End,
                    slot.Status
                })
            .ToList<object>();

                return freeSlots;
            }
        }

        public static List<object> GetFreeCoaches(DateTime Date, int id)
        {
            var allCoaches = CoachService.GetActiveCoaches();

            var BookedCoaches = GetMatches().Where(m => m.SlotId == id && m.Time == Date).Select(m => m.CoachId).ToList();

            var FreeCoaches = allCoaches.Where(c => !BookedCoaches.Contains(c.Id)).Select(c => new
            {
                c.Id,
                c.Name,
            }).ToList<object>();

            return FreeCoaches;
        }

        public static string DeleteMatch(int id)
        {
            using (var db = new TennisContext())
            {
                try
                {
                    // Check if a match with the same slot and time already exists
                    var matchExists = db.Matches.FirstOrDefault(m => m.Id == id);

                    if (matchExists != null)
                    {
                        // If no match with the same slot and time exists, add the new match
                        db.Matches.Remove(matchExists);
                        db.SaveChanges();

                        return "Match deleted successfully!";
                    }

                    return "No match found";
                }
                catch (Exception ex)
                {
                    // If there is a database error, return the specific error message
                    return $"Error: {ex.Message}";
                }
            }
        }
        public static string UpdateMatch(int id)
        {
            using (var db = new TennisContext())
            {
                try
                {
                    // Check if a match with the same slot and time already exists
                    var matchExists = db.Matches.FirstOrDefault(m => m.Id == id);

                    if (matchExists != null)
                    {
                        matchExists.Status = !matchExists.Status;
                        db.SaveChanges();

                        return "Match Status Updated";
                    }

                    return "No match found";
                }
                catch (Exception ex)
                {
                    // If there is a database error, return the specific error message
                    return $"Error: {ex.Message}";
                }
            }
        }
    }
}