﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TennisWeb.CF;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class SlotService
    {
        public static bool CreateSlot(string name,TimeSpan start, TimeSpan end)
        {

            using (var db = new TennisContext())
            {
                Slot newSlot = new Slot
                {
                    Name = name,
                    Start = start,
                    End = end,
                    Status = true,
               };

                db.Slots.Add(newSlot);
                return db.SaveChanges() > 0;
                
            }
        }

        public static bool DeleteSlot(int id)
        {
            using (var db = new TennisContext())
            {
                var slot = db.Slots.Find(id);
                if (slot == null)
                {
                    return false;
                }
                else
                {
                    db.Slots.Remove(slot);
                    return db.SaveChanges() > 0;
                }
            }
        }
        
        public static List<Slot> GetSlots()
        {
            using (var db = new TennisContext())
            {
                return db.Slots.ToList();
            }
        }

        public static List<Slot> GetAllSlots()
        {
            using (var db = new TennisContext())
            {
                return db.Slots.ToList();
            }
        }
    }
}