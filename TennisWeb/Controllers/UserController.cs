using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.Filter;

namespace TennisWeb.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult UserList()
        {
            var users = Services.UserService.GetUsers();
            return View(users);
        }
        
        public ActionResult PlayerList()
        {
            var players = Services.PlayerService.GetPlayers();
            return View(players);
        }

        public ActionResult CoachesList()
        {
            var coaches = Services.CoachService.GetCoaches();
            return View(coaches);
        }
    }
}