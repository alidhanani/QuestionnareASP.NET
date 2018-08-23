using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCQS.Models;
using MCQS.ViewModel;
using System.Collections;

namespace MCQS.Controllers
{
    public class HomeController : Controller
    {
        Random rnd = new Random();
        static Queue later = new Queue();
        static int id = 0;
        static int correct = 0;
        static int questiono = 0;
        int laterCount = 0;
        static bool isEmpty = false;
        static ArrayList qno = new ArrayList();
        static ArrayList ques = new ArrayList();
        McqsDataEntities db = new McqsDataEntities();
        static int count = 0;
        static int count1 = 0;
        static int final = 0;
        static IQueryable<Question> que = null;
        static int counting = 0;
        // GET: Home
        public ActionResult Index(int? id)
        {
            
            
            //int[] done = new int[10];
            

            //      for (int i = 0; i < 10; i++)
            //   {
            h1:
            int num = rnd.Next(1, 11);
            // done[i] = num;

            if (qno.Contains(num))
            {
                goto h1;
            }
            else
            {
                var question = db.Questions.Where(q => q.qno == num);
                que = question;
                qno.Add(num);
                questiono = db.Questions.Where(q => q.qno == num).Select(q => q.qno).Single();
            }

            // }
            count++;
            /* EditQuestion editque = new EditQuestion();
              editque.questions = db.Questions.ToList();
              if (id != null)
                  editque.question = editque.questions.Where(q => q.qno == 2).Single();*/
            /* if (count == 10)
             {
                 End();
             }*/
            /*if(count == 9)
            {
                final = count + later.Count + 1;
            }
            else if(final >= count)
            {
                int value = int.Parse(later.Dequeue().ToString());
                var question = db.Questions.Where(q => q.qno == value);
                que = question;
                if (later.Contains(null))
                {
                    isEmpty = true;
                }
            }
            if (isEmpty)
                return RedirectToAction("End");
            else
                return View(que);*/
            if (count == 10)
                return RedirectToAction("forQue");
            else
                return View(que);
        }

        public ActionResult Next(int qno, int? ans)
        {
            /* UserResond user = new UserResond
             {
                 userId = id,
                 qno = qno,
                 response = ans
             };
             db.UserResonds.Add(user);
             db.SaveChanges();*/
             

            int value = int.Parse(db.Questions.Where(q => q.qno == qno).Select(q => q.ano).Single().ToString());
            if (value == ans)
            {
                UserResond user = new UserResond
                {
                    userId = id,
                    qno = qno,
                    response = ans,
                    sum = 1
                };
                correct++;
                db.UserResonds.Add(user);
                db.SaveChanges();
            } else
            {
                UserResond user = new UserResond
                {
                    userId = id,
                    qno = qno,
                    response = ans,
                    sum = 0
                };
                db.UserResonds.Add(user);
                db.SaveChanges();
            }
         //   db.UserResonds.Add(new UserResond { userId = id });
           // db.UserResonds.Add(new UserResond { qno = qno });
            //db.UserResonds.Add(new UserResond { response = ans });
            //db.UserResonds.SqlQuery("Insert into UserResond(qno, response, userId) values('" + qno + "', '" + ans + "'," + id + ");");
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult front()
        {
            return View();
        }

        public ActionResult addID(int userid)
        {
            id = userid;
            //db.UserResonds.Add(new UserResond { userId = id });
           // db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult End()
        {
            Session["value"] = correct;
            return View();
        }

        /*[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Show(FormCollection form)
        {
            string selectedTechnology = form["Value"];
            if (bool.Parse(db.Questions.Where(q => q.ano == int.Parse(selectedTechnology)).ToString()))
                correct++;
            return Redirect("Index");
        }*/
        public ActionResult Later()
        {
            /*UserResond user1 = new UserResond {
                qno = questiono,
                response = 0,
                userId = id,
                sum = null
            };
            db.UserResonds.Add(user1);
            db.SaveChanges();*/
           later.Enqueue(questiono);
            count1 = later.Count;
            return RedirectToAction("Index");
        }

        public ActionResult forQue()
        {
            if (laterCount < count1)
            {
                int value = int.Parse(later.Dequeue().ToString());
                var question = db.Questions.Where(q => q.qno == value);
                que = question;
                count1--;
                return View(que);
            } else
            {
                return RedirectToAction("End");
            }
            /*counting = later.Count;
            if (counting != 0)
            {
                ques.Add(db.UserResonds.Where(u => u.response == 0).Select(u => u.qno).ToList());
                var question = db.Questions.Where(q => q.qno == ques.);
                que = question;
                return View(que);
            } else
            {
                return RedirectToAction("End");
            }*/

        }
        public ActionResult NextForLater(int qno, int? ans)
        {
            /* UserResond user = new UserResond
             {
                 userId = id,
                 qno = qno,
                 response = ans
             };
             db.UserResonds.Add(user);
             db.SaveChanges();*/


            int value = int.Parse(db.Questions.Where(q => q.qno == qno).Select(q => q.ano).Single().ToString());
            if (value == ans)
            {
                UserResond user = new UserResond
                {
                    userId = id,
                    qno = qno,
                    response = ans,
                    sum = 1
                };
                correct++;
                db.UserResonds.Add(user);
                db.SaveChanges();
            }
            else
            {
                UserResond user = new UserResond
                {
                    userId = id,
                    qno = qno,
                    response = ans,
                    sum = 0
                };
                db.UserResonds.Add(user);
                db.SaveChanges();
            }
            //   db.UserResonds.Add(new UserResond { userId = id });
            // db.UserResonds.Add(new UserResond { qno = qno });
            //db.UserResonds.Add(new UserResond { response = ans });
            //db.UserResonds.SqlQuery("Insert into UserResond(qno, response, userId) values('" + qno + "', '" + ans + "'," + id + ");");
            //db.SaveChanges();
            return RedirectToAction("forQue");
        }
    }

}