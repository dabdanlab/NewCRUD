using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using System.Configuration;
using Animals.App_Start;
using Animals.Models;
using MongoDB.Driver;

namespace Animals.Controllers
{
    public class AnimalsController : Controller
    {
        private AnimalsContext animalsContext;

        private IMongoCollection<AnimalsModel> animalsCollection;

        public AnimalsController()
        {
            animalsContext = new AnimalsContext();
            animalsCollection = animalsContext.database.GetCollection<AnimalsModel>("Animals");
        }
        // GET: Animals
        public ActionResult Index()
        {
            List<AnimalsModel> animals = animalsCollection.AsQueryable<AnimalsModel>().ToList();
            return View(animals);
        }

        // GET: Animals/Details/5
        public ActionResult Details(string id)
        {
            var animalsId = new ObjectId(id);
            var animal = animalsCollection.AsQueryable<AnimalsModel>().SingleOrDefault(x => x.Id == animalsId);
            return View(animal);
        }

        // GET: Animals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        [HttpPost]
        public ActionResult Create(AnimalsModel animals)
        {
            try
            {
                // TODO: Add insert logic here

                animalsCollection.InsertOne(animals);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Animals/Edit/5
        public ActionResult Edit(string id)
        {
            var animalsId = new ObjectId(id);
            var animal = animalsCollection.AsQueryable<AnimalsModel>().SingleOrDefault(x => x.Id == animalsId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, AnimalsModel animals)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<AnimalsModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<AnimalsModel>.Update
                    .Set("Name", animals.Name)
                    .Set("Age", animals.Age);
                var result = animalsCollection.UpdateOne(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Animals/Delete/5
        public ActionResult Delete(string id)
        {
            var animalsId = new ObjectId(id);
            var animal = animalsCollection.AsQueryable<AnimalsModel>().SingleOrDefault(x => x.Id == animalsId);
            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, AnimalsModel animals)
        {
            try
            {
                // TODO: Add delete logic here
                animalsCollection.DeleteOne(Builders<AnimalsModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
