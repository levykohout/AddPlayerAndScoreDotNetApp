
using System;
using System.Collections.Generic;
using System.Linq; //gives access to lambda expression used in CRUD method
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models; //needed to access Player class/object
using WebApplication.Data; //needed to access ApplicationDbContext
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Controllers
{

    //creates PlayerController class inherit Controllers
    public class PlayerController : Controller
    {
        //access database readonly
        private readonly ApplicationDbContext _context;

        //constructor
        public PlayerController(ApplicationDbContext context)
        {
            //sets private _context equal to public context parameter
            _context= context;
        }
        //displays the index
        public IActionResult Index()
        {
            var model=_context.Players.ToList(); //get players list 
            return View(model); //pass them to view
        }
        
        [HttpGet] //method can only be accessed through a get request 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //method can only be accessed through a post request
        public IActionResult Create(Player player)
        {
            _context.Players.Add(player); //add player to the database
            _context.SaveChanges(); //save all changes
            return RedirectToAction("Index"); //redirect to index after save to prevent resubmit
        }

        //details
        public IActionResult Details(int id)
        {
            //grabs the scores of selected player
            var model = _context.Players.Include(e=>e.Scores).FirstOrDefault(e=>e.Id==id);
            return View(model);
        }
        public IActionResult AddScore(int id, int value) 
        {
            _context.Players.Include(e => e.Scores).FirstOrDefault(e => e.Id == id).Scores.Add(new Score{Value = value});
            _context.SaveChanges();
            return RedirectToAction("Details","Player", new {Id = id});
        }

       

        public IActionResult Delete(int id)
        {
            var original = _context.Players.Include(e=>e.Scores).FirstOrDefault(e=>e.Id==id);
            if(original != null){
                _context.Players.Remove(original);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: /Movies/Edit/5
public ActionResult Update(int? id)
{
    // if (id == null)
    // {
    //     return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    // }
    Player player = _context.Players.Include(e=>e.Scores).FirstOrDefault(e=>e.Id==id);
    // if (player == null)
    // {
    //     return HttpNotFound();
    // }
    return View(player);
}


[HttpPost]

 public IActionResult Update(Player player)
        {
            _context.Entry(player).State=EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}