using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class SlotService
    {
        public static bool CreateSlot(string Name, string Start, string End)
        {
            using (var db = new tennisEntities())
            {
                Slot newSlot = new Slot
                {
                    name = Name,
                    time = Start + " - " + End,
                    status = 1,
               };

                db.Slots.Add(newSlot);
                return db.SaveChanges() > 0;
                
            }
        }
    }
}