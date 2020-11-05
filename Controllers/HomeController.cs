using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TTG3.Models;

namespace TTG3.Controllers
{
    public class HomeController : Controller
    {
        private TTG_DB_Entities db = new TTG_DB_Entities();
        public ActionResult Index()
        {
            if(Session["userAdmin"]==null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DashBoard");
            }
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            if(Session["userAdmin"]==null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DashBoard");
            }
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";
            if(Session["userAdmin"]==null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DashBoard");
            }
        }

        public ActionResult Login()
        {
            if(Session["userAdmin"]==null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DashBoard");
            }
        }

        public ActionResult DashBoard()
        {
            if(Session["userAdmin"]!=null)
            {
                //int labCount = db.Locations.Where(a=>a.Room.StartsWith("Lab")).Select(a=>a.Room).Distinct().Count();
                //ViewData["labCount"] = labCount;
                //var model1= db.Locations.Where(a => a.Room.StartsWith("Lab")).Select(a => a.Room).Distinct().ToList();
                //var model = new MyViewModel();
                //model.LabName = db.Locations.Where(a => a.Room.StartsWith("Lab")).Select(a => a.Room).Distinct().ToList();
                //List<string> mymodel = new List<object>();
                //mymodel.Add(db.Locations.Where(a => a.Room.StartsWith("Lab")).Select(a => a.Room).Distinct());
                //ViewBag.labList = db.Locations.Distinct().Where(a=>a.Room.StartsWith("Lab")).GroupBy(a => a.Room).ToList();
                //ViewBag.labList = (from a in db.Locations
                //              select a.Room).Distinct().ToList();
                List<MyViewModel> labList = (from a in db.Locations
                                          select new MyViewModel()
                                          {
                                              LabName= a.Room
                                          }).Where(a=>a.LabName.StartsWith("Lab")).Distinct().ToList();
                ViewBag.labList = labList;
                ViewData["labCount"] = labList.Count();

                List<MyViewModel> roomList = (from a in db.Locations
                             select new MyViewModel()
                             {
                                 RoomName = a.Room
                             }).Where(a => a.RoomName.StartsWith("Chanakya")).Distinct().ToList();
                ViewBag.roomList = roomList;
                ViewData["classroomCount"] = roomList.Count();

                List<MyViewModel> theorySubjectList = (from a in db.Subjects
                                                       select new MyViewModel()
                                                       {
                                                           TCode = a.Subject_Code,
                                                           TName = a.Subject_Name
                                                       }).Where(a => a.TCode.EndsWith("-T")).OrderBy(a=>a.TName).ToList();
                ViewBag.theorySubjectList = theorySubjectList;
                ViewData["theorySubjectCount"] = theorySubjectList.Count();

                List<MyViewModel> practicalSubjectList = (from a in db.Subjects
                                                       select new MyViewModel()
                                                       {
                                                           PCode = a.Subject_Code,
                                                           PName = a.Subject_Name
                                                       }).Where(a => a.PCode.EndsWith("-P")).OrderBy(a=>a.PName).ToList();
                ViewBag.practicalSubjectList = practicalSubjectList;
                ViewData["practicalSubjectCount"] = practicalSubjectList.Count();

                List<MyViewModel> internalProfessorList = (from a in db.Professors.Where(a=>a.Visiting_Faculty=="No")
                                                           select new MyViewModel()
                                                           {
                                                               IProfName = a.Professor_Name
                                                           }).OrderBy(a => a.IProfName).ToList();
                ViewBag.internalProfessorList = internalProfessorList;
                ViewData["internalProfessorCount"] = internalProfessorList.Count();

                List<MyViewModel> externalProfessorList = (from a in db.Professors.Where(a => a.Visiting_Faculty == "Yes")
                                                           select new MyViewModel()
                                                           {
                                                               EProfName = a.Professor_Name
                                                           }).OrderBy(a => a.EProfName).ToList();
                ViewBag.externalProfessorList = externalProfessorList;
                ViewData["externalProfessorCount"] = externalProfessorList.Count();

                ViewData["s1tc"] = db.Sessions
                                   .Where(a => a.Semester_Number == 1 && a.Subject_Type == "Theory")
                                   .Select(a => a.Semester_Number).Count();
                ViewData["s1pc"] = db.Sessions
                                   .Where(a => a.Semester_Number == 1 && a.Subject_Type == "Practical")
                                   .Select(a => a.Semester_Number).Count();
                ViewData["s1ts"] = db.Sessions
                    .Where(a => a.Semester_Number == 1)
                    .Sum(a => a.Sessions_Per_Week);

                ViewData["s2tc"] = db.Sessions
                                   .Where(a => a.Semester_Number == 2 && a.Subject_Type == "Theory")
                                   .Select(a => a.Semester_Number).Count();
                ViewData["s2pc"] = db.Sessions
                                   .Where(a => a.Semester_Number == 2 && a.Subject_Type == "Practical")
                                   .Select(a => a.Semester_Number).Count();
                ViewData["s2ts"] = db.Sessions
                    .Where(a => a.Semester_Number == 2)
                    .Sum(a => a.Sessions_Per_Week);

                ViewData["s3tc"] = db.Sessions
                                   .Where(a => a.Semester_Number == 3 && a.Subject_Type == "Theory")
                                   .Select(a => a.Semester_Number).Count();
                ViewData["s3pc"] = db.Sessions
                                   .Where(a => a.Semester_Number == 3 && a.Subject_Type == "Practical")
                                   .Select(a => a.Semester_Number).Count();
                ViewData["s3ts"] = db.Sessions
                    .Where(a => a.Semester_Number == 3)
                    .Sum(a => a.Sessions_Per_Week);

                ViewData["s4tc"] = db.Sessions
                                   .Where(a => a.Semester_Number == 4 && a.Subject_Type == "Theory")
                                   .Select(a => a.Semester_Number).Count();
                ViewData["s4pc"] = db.Sessions
                                   .Where(a => a.Semester_Number == 4 && a.Subject_Type == "Practical")
                                   .Select(a => a.Semester_Number).Count();
                ViewData["s4ts"] = db.Sessions
                    .Where(a => a.Semester_Number == 4)
                    .Sum(a => a.Sessions_Per_Week);

                ViewData["s5tc"] = db.Sessions
                                   .Where(a => a.Semester_Number == 5 && a.Subject_Type == "Theory")
                                   .Select(a => a.Semester_Number).Count();
                ViewData["s5pc"] = db.Sessions
                                   .Where(a => a.Semester_Number == 5 && a.Subject_Type == "Practical")
                                   .Select(a => a.Semester_Number).Count();
                ViewData["s5ts"] = db.Sessions
                    .Where(a => a.Semester_Number == 5)
                    .Sum(a => a.Sessions_Per_Week);

                //int classroomCount = db.Locations.Where(a => a.Room.StartsWith("Chanakya")).Select(a => a.Room).Distinct().Count();
                //ViewData["classroomCount"] = labCount;
                //var model2 = db.Locations.Select(x => new MyViewModel
                //{
                //    RoomName = x.Room
                //}).Where(a => a.RoomName.StartsWith("Chanakya")).Distinct().ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
                //return View("Login");
            }
        }

        //TTG
        public int Semester_SPW(int sem_number)
        {
            return db.Sessions.Where(a => a.Semester_Number == sem_number).Sum(a => a.Sessions_Per_Week);
        }
        
        public Dictionary<string,string> sem_n_location(int sem_number)
        {
            Dictionary<string, string> sem_location = new Dictionary<string, string>();
            foreach (var a in db.Locations.Where(a => a.Semester_Number == sem_number))
            {
                sem_location.Add(a.Subject_Type, a.Room);
            }
            return sem_location;
        }

        public Dictionary<int,Sem_Data> sem_n_info(int sem_number)
        {
            int count = 1;
            Dictionary<int, Sem_Data> sem_info = new Dictionary<int, Sem_Data>();
            foreach (var a in db.Sessions.Where(a => a.Semester_Number == sem_number))
            {
                sem_info.Add(count, new Sem_Data
                {
                    SubjectID = a.Subject_ID,
                    SubjectName = a.Subject.Subject_Name,
                    SubjectType = a.Subject_Type,
                    ProfessorID = a.Professor_ID,
                    ProfessorName = a.Professor.Professor_Name,
                    count = 0,
                    SPW = a.Sessions_Per_Week,
                    Priority = a.Priority_Level
                });
                count++;
            }
            return sem_info;
        }

        public string session_location(int rn,Dictionary<string,string> location_list,Dictionary<int,Sem_Data> sem_info)
        {
            string location = "";
            for(int j=0;j<location_list.Count;j++)
            {
                if(location_list.ElementAt(j).Key.Equals(sem_info.ElementAt(rn).Value.SubjectType))
                {
                    location = location_list.ElementAt(j).Value;
                    break;
                }
            }
            return location;
        }

        public List<string> sem_n_final_tt(Dictionary<int,Result_Data> sem_n_tt)
        {
            List<string> sem_final_tt = new List<string>();
            int n = 0;
            int num = 0;
            while (n < 50)
            {
                if (n == 0) { sem_final_tt.Add("Time / Day"); }
                else if (n == 1) { sem_final_tt.Add("9.10 AM - 10.30 AM"); }
                else if (n == 2) { sem_final_tt.Add("10.40 AM - 12.00 PM"); }
                else if (n == 3) { sem_final_tt.Add("12.00 PM - 1.00 PM"); }
                else if (n == 4) { sem_final_tt.Add("1.00 PM - 2.20 PM"); }
                else if (n == 5) { sem_final_tt.Add("2.30 PM - 3.50 PM"); }
                else if (n == 6) { sem_final_tt.Add("4.00 PM - 5.20 PM"); }
                else if (n == 7) { sem_final_tt.Add("Monday"); }
                else if (n == 14) { sem_final_tt.Add("Tuesday"); }
                else if (n == 21) { sem_final_tt.Add("Wednesday"); }
                else if (n == 28) { sem_final_tt.Add("Thursday"); }
                else if (n == 35) { sem_final_tt.Add("Friday"); }
                else if (n == 42) { sem_final_tt.Add("Saturday"); }
                else if (n == 10) { sem_final_tt.Add("BREAK"); }
                else if (n == 17) { sem_final_tt.Add("BREAK"); }
                else if (n == 24) { sem_final_tt.Add("BREAK"); }
                else if (n == 31) { sem_final_tt.Add("BREAK"); }
                else if (n == 38) { sem_final_tt.Add("BREAK"); }
                else if (n == 45) { sem_final_tt.Add("BREAK"); }
                else if (n == 43) { sem_final_tt.Add("EXTRA"); }
                else if (n == 44) { sem_final_tt.Add("EXTRA"); }
                else if (n == 46) { sem_final_tt.Add("EXTRA"); }
                else if (n == 47) { sem_final_tt.Add("EXTRA"); }
                else if (n == 48) { sem_final_tt.Add("EXTRA"); }
                else
                {
                    for (int ic = 1; ic < 2; ic++)
                    {
                        if (num < sem_n_tt.Count)
                        {
                            sem_final_tt.Add
                                (
                                    sem_n_tt.ElementAt(num).Value.SubjectName + "\n"
                                    + sem_n_tt.ElementAt(num).Value.ProfessorName + "\n"
                                    + sem_n_tt.ElementAt(num).Value.SessionLocation
                                );
                        }
                        else
                        {
                            sem_final_tt.Add("");
                        }
                    }
                    num++;
                }
                n++;
            }
            return sem_final_tt;
        }

        public ActionResult TimeTableO1()
        {
            if(Session["userAdmin"]!=null)
            {
                int s1tspw = Semester_SPW(1);
                int s2tspw = Semester_SPW(2);
                int s3tspw = Semester_SPW(3);
                int s4tspw = Semester_SPW(4);
                int s5tspw = Semester_SPW(5);
                if(s1tspw==25 && s2tspw==25 && s3tspw==25 && s4tspw==25 && s5tspw==25)
                {
                    ViewData["SuccessTT"] = "Time Table Generation Possible";
                    Dictionary<string, string> sem1_location = sem_n_location(1);
                    Dictionary<string, string> sem2_location = sem_n_location(2);
                    Dictionary<string, string> sem3_location = sem_n_location(3);
                    Dictionary<string, string> sem4_location = sem_n_location(4);
                    Dictionary<string, string> sem5_location = sem_n_location(5);
                    Dictionary<int, Sem_Data> sem1_info = sem_n_info(1);
                    Dictionary<int, Sem_Data> sem2_info = sem_n_info(2);
                    Dictionary<int, Sem_Data> sem3_info = sem_n_info(3);
                    Dictionary<int, Sem_Data> sem4_info = sem_n_info(4);
                    Dictionary<int, Sem_Data> sem5_info = sem_n_info(5);
                    int ttCount = 1, rns1 = 0, rns2 = 0, rns3 = 0, rns4 = 0, rns5 = 0, gc = 0;
                    Dictionary<int, Result_Data> sem1_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem2_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem3_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem4_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem5_tt = new Dictionary<int, Result_Data>();
                    while (ttCount <= 25)
                    {
                        rns1 = Random_Number(sem1_info.Count);
                        if (sem1_info.ElementAt(rns1).Value.count < sem1_info.ElementAt(rns1).Value.SPW)
                        {
                            sem1_tt.Add(ttCount, new Result_Data
                            {
                                SubjectID = sem1_info.ElementAt(rns1).Value.SubjectID,
                                SubjectName = sem1_info.ElementAt(rns1).Value.SubjectName,
                                ProfessorID = sem1_info.ElementAt(rns1).Value.ProfessorID,
                                ProfessorName = sem1_info.ElementAt(rns1).Value.ProfessorName,
                                SessionLocation = session_location(rns1, sem1_location, sem1_info)
                            });
                            sem1_info.ElementAt(rns1).Value.count++;
                            ttCount++;
                        }
                    }
                    ViewBag.Sem_1_TT = sem_n_final_tt(sem1_tt);
                    ttCount = 1;
                    while (ttCount<=25)
                    {
                        rns2 = Random_Number(sem2_info.Count);
                        if (sem2_info.ElementAt(rns2).Value.count < sem2_info.ElementAt(rns2).Value.SPW)
                        {
                            sem2_tt.Add(ttCount, new Result_Data
                            {
                                SubjectID = sem2_info.ElementAt(rns2).Value.SubjectID,
                                SubjectName = sem2_info.ElementAt(rns2).Value.SubjectName,
                                ProfessorID = sem2_info.ElementAt(rns2).Value.ProfessorID,
                                ProfessorName = sem2_info.ElementAt(rns2).Value.ProfessorName,
                                SessionLocation = session_location(rns2, sem2_location, sem2_info)
                            });
                            sem2_info.ElementAt(rns2).Value.count++;
                            ttCount++;
                        }
                    }
                    ViewBag.Sem_2_TT = sem_n_final_tt(sem2_tt);
                    ttCount = 1;
                    while (ttCount <= 25)
                    {
                        rns3 = Random_Number(sem3_info.Count);
                        if (sem3_info.ElementAt(rns3).Value.count < sem3_info.ElementAt(rns3).Value.SPW)
                        {
                            sem3_tt.Add(ttCount, new Result_Data
                            {
                                SubjectID = sem3_info.ElementAt(rns3).Value.SubjectID,
                                SubjectName = sem3_info.ElementAt(rns3).Value.SubjectName,
                                ProfessorID = sem3_info.ElementAt(rns3).Value.ProfessorID,
                                ProfessorName = sem3_info.ElementAt(rns3).Value.ProfessorName,
                                SessionLocation = session_location(rns3, sem3_location, sem3_info)
                            });
                            sem3_info.ElementAt(rns3).Value.count++;
                            ttCount++;
                        }
                    }
                    ViewBag.Sem_3_TT = sem_n_final_tt(sem3_tt);
                    ttCount = 1;
                    while(ttCount<=25)
                    {
                        rns4 = Random_Number(sem4_info.Count);
                        if(sem4_info.ElementAt(rns4).Value.count<sem4_info.ElementAt(rns4).Value.SPW)
                        {
                            sem4_tt.Add(ttCount, new Result_Data
                            {
                                SubjectID = sem4_info.ElementAt(rns4).Value.SubjectID,
                                SubjectName = sem4_info.ElementAt(rns4).Value.SubjectName,
                                ProfessorID = sem4_info.ElementAt(rns4).Value.ProfessorID,
                                ProfessorName = sem4_info.ElementAt(rns4).Value.ProfessorName,
                                SessionLocation = session_location(rns4, sem4_location, sem4_info)
                            });
                            sem4_info.ElementAt(rns4).Value.count++;
                            ttCount++;
                        }
                    }
                    ViewBag.Sem_4_TT = sem_n_final_tt(sem4_tt);
                    ttCount = 1;
                    while (ttCount <= 25)
                    {
                        rns5 = Random_Number(sem5_info.Count);
                        if (sem5_info.ElementAt(rns5).Value.count < sem5_info.ElementAt(rns5).Value.SPW)
                        {
                            sem5_tt.Add(ttCount, new Result_Data
                            {
                                SubjectID = sem5_info.ElementAt(rns5).Value.SubjectID,
                                SubjectName = sem5_info.ElementAt(rns5).Value.SubjectName,
                                ProfessorID = sem5_info.ElementAt(rns5).Value.ProfessorID,
                                ProfessorName = sem5_info.ElementAt(rns5).Value.ProfessorName,
                                SessionLocation = session_location(rns5, sem5_location, sem5_info)
                            });
                            sem5_info.ElementAt(rns5).Value.count++;
                            ttCount++;
                        }
                    }
                    ViewBag.Sem_5_TT = sem_n_final_tt(sem5_tt);
                }
                else
                {
                    if (s1tspw != 25)
                    {
                        ViewData["Sem1TS"] = "Semester 1 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s1tspw;
                    }
                    if (s3tspw != 25)
                    {
                        ViewData["Sem3TS"] = "Semester 3 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s3tspw;
                    }
                    if (s5tspw != 25)
                    {
                        ViewData["Sem5TS"] = "Semester 5 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s5tspw;
                    }
                    if (s2tspw != 25)
                    {
                        ViewData["Sem2TS"] = "Semester 2 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s2tspw;
                    }
                    if (s4tspw != 25)
                    {
                        ViewData["Sem4TS"] = "Semester 4 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s4tspw;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult TimeTableO2()
        {
            if(Session["userAdmin"]!=null)
            {
                int s1tspw = Semester_SPW(1);
                int s2tspw = Semester_SPW(2);
                int s3tspw = Semester_SPW(3);
                int s4tspw = Semester_SPW(4);
                int s5tspw = Semester_SPW(5);

                if (s1tspw==25 && s3tspw==25 && s5tspw==25 && s2tspw==25 && s4tspw==25)
                {
                    ViewData["SuccessTT"] = "Time Table Generation Possible";
                    Dictionary<string, string> sem1_location = sem_n_location(1);
                    Dictionary<string, string> sem2_location = sem_n_location(2);
                    Dictionary<string, string> sem3_location = sem_n_location(3);
                    Dictionary<string, string> sem4_location = sem_n_location(4);
                    Dictionary<string, string> sem5_location = sem_n_location(5);
                    Dictionary<int, Sem_Data> sem1_info = sem_n_info(1);
                    Dictionary<int, Sem_Data> sem2_info = sem_n_info(2);
                    Dictionary<int, Sem_Data> sem3_info = sem_n_info(3);
                    Dictionary<int, Sem_Data> sem4_info = sem_n_info(4);
                    Dictionary<int, Sem_Data> sem5_info = sem_n_info(5);
                    //TTG
                    int ttCount = 1, rns1 = 0, rns2 = 0, rns3 = 0, rns4 = 0, rns5 = 0, gc = 0;
                    Dictionary<int, Result_Data> sem1_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem2_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem3_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem4_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem5_tt = new Dictionary<int, Result_Data>();

                    while (ttCount <= 25)
                    {
                        rns2 = Random_Number(sem2_info.Count);
                        rns4 = Random_Number(sem4_info.Count);
                        if (sem2_info.ElementAt(rns2).Value.count < sem2_info.ElementAt(rns2).Value.SPW
                            &&
                            sem4_info.ElementAt(rns4).Value.count < sem4_info.ElementAt(rns4).Value.SPW
                            &&
                            sem2_info.ElementAt(rns2).Value.ProfessorID != sem4_info.ElementAt(rns4).Value.ProfessorID)
                        {
                            sem2_tt.Add(ttCount, new Result_Data
                            {
                                SubjectID = sem2_info.ElementAt(rns2).Value.SubjectID,
                                SubjectName = sem2_info.ElementAt(rns2).Value.SubjectName,
                                ProfessorID = sem2_info.ElementAt(rns2).Value.ProfessorID,
                                ProfessorName = sem2_info.ElementAt(rns2).Value.ProfessorName,
                                SessionLocation = session_location(rns2,sem2_location,sem2_info)
                            });
                            sem2_info.ElementAt(rns2).Value.count++;
                            sem4_tt.Add(ttCount, new Result_Data
                            {
                                SubjectID = sem4_info.ElementAt(rns4).Value.SubjectID,
                                SubjectName = sem4_info.ElementAt(rns4).Value.SubjectName,
                                ProfessorID = sem4_info.ElementAt(rns4).Value.ProfessorID,
                                ProfessorName = sem4_info.ElementAt(rns4).Value.ProfessorName,
                                SessionLocation = session_location(rns4, sem4_location, sem4_info)
                            });
                            sem4_info.ElementAt(rns4).Value.count++;
                            ttCount++;
                        }
                        else
                        {
                            gc++;
                        }
                        if(gc>1000000)
                        {
                            ViewBag.TryAgainMsgEven = "Issue generating Time Table for Semester 2 and Semester 4";
                            break;
                        }
                    }
                    ViewBag.Sem_2_TT = sem_n_final_tt(sem2_tt);
                    ViewBag.Sem_4_TT = sem_n_final_tt(sem4_tt);

                    ttCount = 1;
                    gc = 0;
                    while (ttCount <= 25)
                    {
                        rns1 = Random_Number(sem1_info.Count);
                        rns3 = Random_Number(sem3_info.Count);
                        rns5 = Random_Number(sem5_info.Count);
                        if (sem1_info.ElementAt(rns1).Value.count < sem1_info.ElementAt(rns1).Value.SPW
                            &&
                            sem3_info.ElementAt(rns3).Value.count < sem3_info.ElementAt(rns3).Value.SPW
                            &&
                            sem5_info.ElementAt(rns5).Value.count < sem5_info.ElementAt(rns5).Value.SPW)
                        {
                            if (sem1_info.ElementAt(rns1).Value.ProfessorID != sem3_info.ElementAt(rns3).Value.ProfessorID
                                &&
                                sem3_info.ElementAt(rns3).Value.ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID
                                &&
                                sem5_info.ElementAt(rns5).Value.ProfessorID != sem1_info.ElementAt(rns1).Value.ProfessorID)
                            {
                                sem1_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem1_info.ElementAt(rns1).Value.SubjectID,
                                    SubjectName = sem1_info.ElementAt(rns1).Value.SubjectName,
                                    ProfessorID = sem1_info.ElementAt(rns1).Value.ProfessorID,
                                    ProfessorName = sem1_info.ElementAt(rns1).Value.ProfessorName,
                                    SessionLocation = session_location(rns1, sem1_location, sem1_info)
                                });
                                sem1_info.ElementAt(rns1).Value.count++;
                                sem3_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem3_info.ElementAt(rns3).Value.SubjectID,
                                    SubjectName = sem3_info.ElementAt(rns3).Value.SubjectName,
                                    ProfessorID = sem3_info.ElementAt(rns3).Value.ProfessorID,
                                    ProfessorName = sem3_info.ElementAt(rns3).Value.ProfessorName,
                                    SessionLocation = session_location(rns3, sem3_location, sem3_info)
                                });
                                sem3_info.ElementAt(rns3).Value.count++;
                                sem5_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem5_info.ElementAt(rns5).Value.SubjectID,
                                    SubjectName = sem5_info.ElementAt(rns5).Value.SubjectName,
                                    ProfessorID = sem5_info.ElementAt(rns5).Value.ProfessorID,
                                    ProfessorName = sem5_info.ElementAt(rns5).Value.ProfessorName,
                                    SessionLocation = session_location(rns5, sem5_location, sem5_info)
                                });
                                sem5_info.ElementAt(rns5).Value.count++;
                                ttCount++;
                            }
                        }
                        else
                        {
                            gc++;
                        }
                        if (gc > 9000000)
                        {
                            ViewBag.TryAgainMsgOdd = "Issue generating Time Table for Semester 1 , Semester 3 and Semester 5";
                            break;
                        }
                    }
                    ViewBag.Sem_1_TT = sem_n_final_tt(sem1_tt);
                    ViewBag.Sem_3_TT = sem_n_final_tt(sem3_tt);
                    ViewBag.Sem_5_TT = sem_n_final_tt(sem5_tt);


                    //Response.Write("<br/><br/><br/>Sem3");
                    //for (int i = 0; i < sem3_tt.Count; i++)
                    //{
                    //    Response.Write("<br/><br/>Subject Name : " + sem3_tt.ElementAt(i).Value.SubjectName);
                    //    Response.Write("     Professor Name : " + sem3_tt.ElementAt(i).Value.ProfessorName);
                    //    Response.Write("     Location : " + sem3_tt.ElementAt(i).Value.SessionLocation);
                    //    Response.Write("     Key : " + sem1_tt.ElementAt(i).Key);
                    //}
                    //S3
                    //Response.Write("<br/>Sem5");
                    //for (int i = 0; i < sem5_tt.Count; i++)
                    //{
                    //    Response.Write("<br/><br/>Subject Name : " + sem5_tt.ElementAt(i).Value.SubjectName);
                    //    Response.Write("<br/>Professor Name : " + sem5_tt.ElementAt(i).Value.ProfessorName);
                    //    Response.Write("<br/>Location : " + sem5_tt.ElementAt(i).Value.SessionLocation);
                    //    Response.Write("     Iteration : " + i);
                    //}
                    
                    //TTG
                }
                else
                {
                    if(s1tspw!=25)
                    {
                        ViewData["Sem1TS"] = "Semester 1 Total Session Per Week not equals to 25\nTotal Session Per Week : "+s1tspw;
                    }
                    if(s3tspw!=25)
                    {
                        ViewData["Sem3TS"] = "Semester 3 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s3tspw;
                    }
                    if (s5tspw!=25)
                    {
                        ViewData["Sem5TS"] = "Semester 5 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s5tspw;
                    }
                    if (s2tspw != 25)
                    {
                        ViewData["Sem2TS"] = "Semester 2 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s2tspw;
                    }
                    if (s4tspw != 25)
                    {
                        ViewData["Sem4TS"] = "Semester 4 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s4tspw;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult TimeTableO3()
        {
            if(Session["userAdmin"]!=null)
            {
                int s1tspw = Semester_SPW(1);
                int s2tspw = Semester_SPW(2);
                int s3tspw = Semester_SPW(3);
                int s4tspw = Semester_SPW(4);
                int s5tspw = Semester_SPW(5);
                if(s1tspw==25 && s2tspw==25 && s3tspw==25 && s4tspw==25 && s5tspw==25)
                {
                    ViewData["SuccessTT"] = "Time Table Generation Possible";
                    Dictionary<string, string> sem1_location = sem_n_location(1);
                    Dictionary<string, string> sem2_location = sem_n_location(2);
                    Dictionary<string, string> sem3_location = sem_n_location(3);
                    Dictionary<string, string> sem4_location = sem_n_location(4);
                    Dictionary<string, string> sem5_location = sem_n_location(5);
                    Dictionary<int, Sem_Data> sem1_info = sem_n_info(1);
                    Dictionary<int, Sem_Data> sem2_info = sem_n_info(2);
                    Dictionary<int, Sem_Data> sem3_info = sem_n_info(3);
                    Dictionary<int, Sem_Data> sem4_info = sem_n_info(4);
                    Dictionary<int, Sem_Data> sem5_info = sem_n_info(5);
                    int ttCount = 1, rns1 = 0, rns2 = 0, rns3 = 0, rns4 = 0, rns5 = 0, gc = 0;
                    Dictionary<int, Result_Data> sem1_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem2_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem3_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem4_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem5_tt = new Dictionary<int, Result_Data>();

                    //S1
                    while (ttCount <= 25)
                    {
                        rns1 = Random_Number(sem1_info.Count);
                        if (ttCount > 2)
                        {
                            if (sem1_info.ElementAt(rns1).Value.count < sem1_info.ElementAt(rns1).Value.SPW
                                &&
                                sem1_tt[ttCount-1].ProfessorID != sem1_info.ElementAt(rns1).Value.ProfessorID
                                &&
                                sem1_tt[ttCount-2].ProfessorID != sem1_info.ElementAt(rns1).Value.ProfessorID)
                            {
                                sem1_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem1_info.ElementAt(rns1).Value.SubjectID,
                                    SubjectName = sem1_info.ElementAt(rns1).Value.SubjectName,
                                    ProfessorID = sem1_info.ElementAt(rns1).Value.ProfessorID,
                                    ProfessorName = sem1_info.ElementAt(rns1).Value.ProfessorName,
                                    SessionLocation = session_location(rns1, sem1_location, sem1_info)
                                });
                                sem1_info.ElementAt(rns1).Value.count++;
                                ttCount++;
                                if (sem1_info.ElementAt(rns1).Value.Priority == 1
                                    &&
                                    sem1_info.ElementAt(rns1).Value.count < sem1_info.ElementAt(rns1).Value.SPW)
                                {
                                    sem1_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem1_info.ElementAt(rns1).Value.SubjectID,
                                        SubjectName = sem1_info.ElementAt(rns1).Value.SubjectName,
                                        ProfessorID = sem1_info.ElementAt(rns1).Value.ProfessorID,
                                        ProfessorName = sem1_info.ElementAt(rns1).Value.ProfessorName,
                                        SessionLocation = session_location(rns1, sem1_location, sem1_info)
                                    });
                                    sem1_info.ElementAt(rns1).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        else
                        {
                            if (sem1_info.ElementAt(rns1).Value.count < sem1_info.ElementAt(rns1).Value.SPW)
                            {
                                sem1_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem1_info.ElementAt(rns1).Value.SubjectID,
                                    SubjectName = sem1_info.ElementAt(rns1).Value.SubjectName,
                                    ProfessorID = sem1_info.ElementAt(rns1).Value.ProfessorID,
                                    ProfessorName = sem1_info.ElementAt(rns1).Value.ProfessorName,
                                    SessionLocation = session_location(rns1, sem1_location, sem1_info)
                                });
                                sem1_info.ElementAt(rns1).Value.count++;
                                ttCount++;
                                if (sem1_info.ElementAt(rns1).Value.Priority == 1
                                    &&
                                    sem1_info.ElementAt(rns1).Value.count < sem1_info.ElementAt(rns1).Value.SPW)
                                {
                                    sem1_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem1_info.ElementAt(rns1).Value.SubjectID,
                                        SubjectName = sem1_info.ElementAt(rns1).Value.SubjectName,
                                        ProfessorID = sem1_info.ElementAt(rns1).Value.ProfessorID,
                                        ProfessorName = sem1_info.ElementAt(rns1).Value.ProfessorName,
                                        SessionLocation = session_location(rns1, sem1_location, sem1_info)
                                    });
                                    sem1_info.ElementAt(rns1).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        if (gc > 2000000)
                        {
                            ViewBag.TryAgainS1 = "Issue generating Time Table for Semester 1";
                            break;
                        }
                    }
                    ViewBag.Sem_1_TT = sem_n_final_tt(sem1_tt);
                    //S1
                    //S2
                    ttCount = 1;
                    gc = 0;
                    while (ttCount <= 25)
                    {
                        rns2 = Random_Number(sem2_info.Count);
                        if (ttCount > 2)
                        {
                            if (sem2_info.ElementAt(rns2).Value.count < sem2_info.ElementAt(rns2).Value.SPW
                                &&
                                sem2_tt[ttCount - 1].ProfessorID != sem2_info.ElementAt(rns2).Value.ProfessorID
                                &&
                                sem2_tt[ttCount - 2].ProfessorID != sem2_info.ElementAt(rns2).Value.ProfessorID)
                            {
                                sem2_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem2_info.ElementAt(rns2).Value.SubjectID,
                                    SubjectName = sem2_info.ElementAt(rns2).Value.SubjectName,
                                    ProfessorID = sem2_info.ElementAt(rns2).Value.ProfessorID,
                                    ProfessorName = sem2_info.ElementAt(rns2).Value.ProfessorName,
                                    SessionLocation = session_location(rns2, sem2_location, sem2_info)
                                });
                                sem2_info.ElementAt(rns2).Value.count++;
                                ttCount++;
                                if (sem2_info.ElementAt(rns2).Value.Priority == 1
                                    &&
                                    sem2_info.ElementAt(rns2).Value.count < sem2_info.ElementAt(rns2).Value.SPW)
                                {
                                    sem2_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem2_info.ElementAt(rns2).Value.SubjectID,
                                        SubjectName = sem2_info.ElementAt(rns2).Value.SubjectName,
                                        ProfessorID = sem2_info.ElementAt(rns2).Value.ProfessorID,
                                        ProfessorName = sem2_info.ElementAt(rns2).Value.ProfessorName,
                                        SessionLocation = session_location(rns2, sem2_location, sem2_info)
                                    });
                                    sem2_info.ElementAt(rns2).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        else
                        {
                            if (sem2_info.ElementAt(rns2).Value.count < sem2_info.ElementAt(rns2).Value.SPW)
                            {
                                sem2_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem2_info.ElementAt(rns2).Value.SubjectID,
                                    SubjectName = sem2_info.ElementAt(rns2).Value.SubjectName,
                                    ProfessorID = sem2_info.ElementAt(rns2).Value.ProfessorID,
                                    ProfessorName = sem2_info.ElementAt(rns2).Value.ProfessorName,
                                    SessionLocation = session_location(rns2, sem2_location, sem2_info)
                                });
                                sem2_info.ElementAt(rns2).Value.count++;
                                ttCount++;
                                if (sem2_info.ElementAt(rns2).Value.Priority == 1
                                    &&
                                    sem2_info.ElementAt(rns2).Value.count < sem2_info.ElementAt(rns2).Value.SPW)
                                {
                                    sem2_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem2_info.ElementAt(rns2).Value.SubjectID,
                                        SubjectName = sem2_info.ElementAt(rns2).Value.SubjectName,
                                        ProfessorID = sem2_info.ElementAt(rns2).Value.ProfessorID,
                                        ProfessorName = sem2_info.ElementAt(rns2).Value.ProfessorName,
                                        SessionLocation = session_location(rns2, sem2_location, sem2_info)
                                    });
                                    sem2_info.ElementAt(rns2).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        if (gc > 2000000)
                        {
                            ViewBag.TryAgainS2 = "Issue generating Time Table for Semester 2";
                            break;
                        }
                    }
                    ViewBag.Sem_2_TT = sem_n_final_tt(sem2_tt);
                    //S2
                    //S3
                    ttCount = 1;
                    gc = 0;
                    while (ttCount <= 25)
                    {
                        rns3 = Random_Number(sem3_info.Count);
                        if (ttCount > 2)
                        {
                            if (sem3_info.ElementAt(rns3).Value.count < sem3_info.ElementAt(rns3).Value.SPW
                                &&
                                sem3_tt[ttCount - 1].ProfessorID != sem3_info.ElementAt(rns3).Value.ProfessorID
                                &&
                                sem3_tt[ttCount - 2].ProfessorID != sem3_info.ElementAt(rns3).Value.ProfessorID)
                            {
                                sem3_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem3_info.ElementAt(rns3).Value.SubjectID,
                                    SubjectName = sem3_info.ElementAt(rns3).Value.SubjectName,
                                    ProfessorID = sem3_info.ElementAt(rns3).Value.ProfessorID,
                                    ProfessorName = sem3_info.ElementAt(rns3).Value.ProfessorName,
                                    SessionLocation = session_location(rns3, sem3_location, sem3_info)
                                });
                                sem3_info.ElementAt(rns3).Value.count++;
                                ttCount++;
                                if (sem3_info.ElementAt(rns3).Value.Priority == 1
                                    &&
                                    sem3_info.ElementAt(rns3).Value.count < sem3_info.ElementAt(rns3).Value.SPW)
                                {
                                    sem3_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem3_info.ElementAt(rns3).Value.SubjectID,
                                        SubjectName = sem3_info.ElementAt(rns3).Value.SubjectName,
                                        ProfessorID = sem3_info.ElementAt(rns3).Value.ProfessorID,
                                        ProfessorName = sem3_info.ElementAt(rns3).Value.ProfessorName,
                                        SessionLocation = session_location(rns3, sem3_location, sem3_info)
                                    });
                                    sem3_info.ElementAt(rns3).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        else
                        {
                            if (sem3_info.ElementAt(rns3).Value.count < sem3_info.ElementAt(rns3).Value.SPW)
                            {
                                sem3_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem3_info.ElementAt(rns3).Value.SubjectID,
                                    SubjectName = sem3_info.ElementAt(rns3).Value.SubjectName,
                                    ProfessorID = sem3_info.ElementAt(rns3).Value.ProfessorID,
                                    ProfessorName = sem3_info.ElementAt(rns3).Value.ProfessorName,
                                    SessionLocation = session_location(rns3, sem3_location, sem3_info)
                                });
                                sem3_info.ElementAt(rns3).Value.count++;
                                ttCount++;
                                if (sem3_info.ElementAt(rns3).Value.Priority == 1
                                    &&
                                    sem3_info.ElementAt(rns3).Value.count < sem3_info.ElementAt(rns3).Value.SPW)
                                {
                                    sem3_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem3_info.ElementAt(rns3).Value.SubjectID,
                                        SubjectName = sem3_info.ElementAt(rns3).Value.SubjectName,
                                        ProfessorID = sem3_info.ElementAt(rns3).Value.ProfessorID,
                                        ProfessorName = sem3_info.ElementAt(rns3).Value.ProfessorName,
                                        SessionLocation = session_location(rns3, sem3_location, sem3_info)
                                    });
                                    sem3_info.ElementAt(rns3).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        if (gc > 2000000)
                        {
                            ViewBag.TryAgainS3 = "Issue generating Time Table for Semester 3";
                            break;
                        }
                    }
                    ViewBag.Sem_3_TT = sem_n_final_tt(sem3_tt);
                    //S3
                    //S4
                    ttCount = 1;
                    gc = 0;
                    while (ttCount <= 25)
                    {
                        rns4 = Random_Number(sem4_info.Count);
                        if (ttCount > 2)
                        {
                            if (sem4_info.ElementAt(rns4).Value.count < sem4_info.ElementAt(rns4).Value.SPW
                                &&
                                sem4_tt[ttCount - 1].ProfessorID != sem4_info.ElementAt(rns4).Value.ProfessorID
                                &&
                                sem4_tt[ttCount - 2].ProfessorID != sem4_info.ElementAt(rns4).Value.ProfessorID)
                            {
                                sem4_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem4_info.ElementAt(rns4).Value.SubjectID,
                                    SubjectName = sem4_info.ElementAt(rns4).Value.SubjectName,
                                    ProfessorID = sem4_info.ElementAt(rns4).Value.ProfessorID,
                                    ProfessorName = sem4_info.ElementAt(rns4).Value.ProfessorName,
                                    SessionLocation = session_location(rns4, sem4_location, sem4_info)
                                });
                                sem4_info.ElementAt(rns4).Value.count++;
                                ttCount++;
                                if (sem4_info.ElementAt(rns4).Value.Priority == 1
                                    &&
                                    sem4_info.ElementAt(rns4).Value.count < sem4_info.ElementAt(rns4).Value.SPW)
                                {
                                    sem4_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem4_info.ElementAt(rns4).Value.SubjectID,
                                        SubjectName = sem4_info.ElementAt(rns4).Value.SubjectName,
                                        ProfessorID = sem4_info.ElementAt(rns4).Value.ProfessorID,
                                        ProfessorName = sem4_info.ElementAt(rns4).Value.ProfessorName,
                                        SessionLocation = session_location(rns4, sem4_location, sem4_info)
                                    });
                                    sem4_info.ElementAt(rns4).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        else
                        {
                            if (sem4_info.ElementAt(rns4).Value.count < sem4_info.ElementAt(rns4).Value.SPW)
                            {
                                sem4_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem4_info.ElementAt(rns4).Value.SubjectID,
                                    SubjectName = sem4_info.ElementAt(rns4).Value.SubjectName,
                                    ProfessorID = sem4_info.ElementAt(rns4).Value.ProfessorID,
                                    ProfessorName = sem4_info.ElementAt(rns4).Value.ProfessorName,
                                    SessionLocation = session_location(rns4, sem4_location, sem4_info)
                                });
                                sem4_info.ElementAt(rns4).Value.count++;
                                ttCount++;
                                if (sem4_info.ElementAt(rns4).Value.Priority == 1
                                    &&
                                    sem4_info.ElementAt(rns4).Value.count < sem4_info.ElementAt(rns4).Value.SPW)
                                {
                                    sem4_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem4_info.ElementAt(rns4).Value.SubjectID,
                                        SubjectName = sem4_info.ElementAt(rns4).Value.SubjectName,
                                        ProfessorID = sem4_info.ElementAt(rns4).Value.ProfessorID,
                                        ProfessorName = sem4_info.ElementAt(rns4).Value.ProfessorName,
                                        SessionLocation = session_location(rns4, sem4_location, sem4_info)
                                    });
                                    sem4_info.ElementAt(rns4).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        if (gc > 2000000)
                        {
                            ViewBag.TryAgainS4 = "Issue generating Time Table for Semester 4";
                            break;
                        }
                    }
                    ViewBag.Sem_4_TT = sem_n_final_tt(sem4_tt);
                    //S4
                    //S5
                    ttCount = 1;
                    gc = 0;
                    while (ttCount <= 25)
                    {
                        rns5 = Random_Number(sem5_info.Count);
                        if (ttCount > 2)
                        {
                            if (sem5_info.ElementAt(rns5).Value.count < sem5_info.ElementAt(rns5).Value.SPW
                                &&
                                sem5_tt[ttCount - 1].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID
                                &&
                                sem5_tt[ttCount - 2].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID)
                            {
                                sem5_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem5_info.ElementAt(rns5).Value.SubjectID,
                                    SubjectName = sem5_info.ElementAt(rns5).Value.SubjectName,
                                    ProfessorID = sem5_info.ElementAt(rns5).Value.ProfessorID,
                                    ProfessorName = sem5_info.ElementAt(rns5).Value.ProfessorName,
                                    SessionLocation = session_location(rns5, sem5_location, sem5_info)
                                });
                                sem5_info.ElementAt(rns5).Value.count++;
                                ttCount++;
                                if (sem5_info.ElementAt(rns5).Value.Priority == 1
                                    &&
                                    sem5_info.ElementAt(rns5).Value.count < sem5_info.ElementAt(rns5).Value.SPW)
                                {
                                    sem5_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem5_info.ElementAt(rns5).Value.SubjectID,
                                        SubjectName = sem5_info.ElementAt(rns5).Value.SubjectName,
                                        ProfessorID = sem5_info.ElementAt(rns5).Value.ProfessorID,
                                        ProfessorName = sem5_info.ElementAt(rns5).Value.ProfessorName,
                                        SessionLocation = session_location(rns5, sem5_location, sem5_info)
                                    });
                                    sem5_info.ElementAt(rns5).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        else
                        {
                            if (sem5_info.ElementAt(rns5).Value.count < sem5_info.ElementAt(rns5).Value.SPW)
                            {
                                sem5_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem5_info.ElementAt(rns5).Value.SubjectID,
                                    SubjectName = sem5_info.ElementAt(rns5).Value.SubjectName,
                                    ProfessorID = sem5_info.ElementAt(rns5).Value.ProfessorID,
                                    ProfessorName = sem5_info.ElementAt(rns5).Value.ProfessorName,
                                    SessionLocation = session_location(rns5, sem5_location, sem5_info)
                                });
                                sem5_info.ElementAt(rns5).Value.count++;
                                ttCount++;
                                if (sem5_info.ElementAt(rns5).Value.Priority == 1
                                    &&
                                    sem5_info.ElementAt(rns5).Value.count < sem5_info.ElementAt(rns5).Value.SPW)
                                {
                                    sem5_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem5_info.ElementAt(rns5).Value.SubjectID,
                                        SubjectName = sem5_info.ElementAt(rns5).Value.SubjectName,
                                        ProfessorID = sem5_info.ElementAt(rns5).Value.ProfessorID,
                                        ProfessorName = sem5_info.ElementAt(rns5).Value.ProfessorName,
                                        SessionLocation = session_location(rns5, sem5_location, sem5_info)
                                    });
                                    sem5_info.ElementAt(rns5).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        if (gc > 2000000)
                        {
                            ViewBag.TryAgainS5 = "Issue generating Time Table for Semester 5";
                            break;
                        }
                    }
                    ViewBag.Sem_5_TT = sem_n_final_tt(sem5_tt);
                    //S5
                }
                else
                {
                    if (s1tspw != 25)
                    {
                        ViewData["Sem1TS"] = "Semester 1 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s1tspw;
                    }
                    if (s3tspw != 25)
                    {
                        ViewData["Sem3TS"] = "Semester 3 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s3tspw;
                    }
                    if (s5tspw != 25)
                    {
                        ViewData["Sem5TS"] = "Semester 5 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s5tspw;
                    }
                    if (s2tspw != 25)
                    {
                        ViewData["Sem2TS"] = "Semester 2 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s2tspw;
                    }
                    if (s4tspw != 25)
                    {
                        ViewData["Sem4TS"] = "Semester 4 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s4tspw;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult TimeTableO4()
        {
            if (Session["userAdmin"] != null)
            {
                int s1tspw = Semester_SPW(1);
                int s2tspw = Semester_SPW(2);
                int s3tspw = Semester_SPW(3);
                int s4tspw = Semester_SPW(4);
                int s5tspw = Semester_SPW(5);
                if (s1tspw == 25 && s2tspw == 25 && s3tspw == 25 && s4tspw == 25 && s5tspw == 25)
                {
                    ViewData["SuccessTT"] = "Time Table Generation Possible";
                    Dictionary<string, string> sem1_location = sem_n_location(1);
                    Dictionary<string, string> sem2_location = sem_n_location(2);
                    Dictionary<string, string> sem3_location = sem_n_location(3);
                    Dictionary<string, string> sem4_location = sem_n_location(4);
                    Dictionary<string, string> sem5_location = sem_n_location(5);
                    Dictionary<int, Sem_Data> sem1_info = sem_n_info(1);
                    Dictionary<int, Sem_Data> sem2_info = sem_n_info(2);
                    Dictionary<int, Sem_Data> sem3_info = sem_n_info(3);
                    Dictionary<int, Sem_Data> sem4_info = sem_n_info(4);
                    Dictionary<int, Sem_Data> sem5_info = sem_n_info(5);
                    int ttCount = 1, rns1 = 0, rns2 = 0, rns3 = 0, rns4 = 0, rns5 = 0, gc = 0;
                    Dictionary<int, Result_Data> sem1_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem2_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem3_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem4_tt = new Dictionary<int, Result_Data>();
                    Dictionary<int, Result_Data> sem5_tt = new Dictionary<int, Result_Data>();

                    //S2
                    while (ttCount <= 25)
                    {
                        rns2 = Random_Number(sem2_info.Count);
                        if (ttCount > 2)
                        {
                            if (sem2_info.ElementAt(rns2).Value.count < sem2_info.ElementAt(rns2).Value.SPW
                                &&
                                sem2_tt[ttCount - 1].ProfessorID != sem2_info.ElementAt(rns2).Value.ProfessorID
                                &&
                                sem2_tt[ttCount - 2].ProfessorID != sem2_info.ElementAt(rns2).Value.ProfessorID)
                            {
                                sem2_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem2_info.ElementAt(rns2).Value.SubjectID,
                                    SubjectName = sem2_info.ElementAt(rns2).Value.SubjectName,
                                    ProfessorID = sem2_info.ElementAt(rns2).Value.ProfessorID,
                                    ProfessorName = sem2_info.ElementAt(rns2).Value.ProfessorName,
                                    SessionLocation = session_location(rns2, sem2_location, sem2_info)
                                });
                                sem2_info.ElementAt(rns2).Value.count++;
                                ttCount++;
                                if (sem2_info.ElementAt(rns2).Value.Priority == 1
                                    &&
                                    sem2_info.ElementAt(rns2).Value.count < sem2_info.ElementAt(rns2).Value.SPW)
                                {
                                    sem2_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem2_info.ElementAt(rns2).Value.SubjectID,
                                        SubjectName = sem2_info.ElementAt(rns2).Value.SubjectName,
                                        ProfessorID = sem2_info.ElementAt(rns2).Value.ProfessorID,
                                        ProfessorName = sem2_info.ElementAt(rns2).Value.ProfessorName,
                                        SessionLocation = session_location(rns2, sem2_location, sem2_info)
                                    });
                                    sem2_info.ElementAt(rns2).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        else
                        {
                            if (sem2_info.ElementAt(rns2).Value.count < sem2_info.ElementAt(rns2).Value.SPW)
                            {
                                sem2_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem2_info.ElementAt(rns2).Value.SubjectID,
                                    SubjectName = sem2_info.ElementAt(rns2).Value.SubjectName,
                                    ProfessorID = sem2_info.ElementAt(rns2).Value.ProfessorID,
                                    ProfessorName = sem2_info.ElementAt(rns2).Value.ProfessorName,
                                    SessionLocation = session_location(rns2, sem2_location, sem2_info)
                                });
                                sem2_info.ElementAt(rns2).Value.count++;
                                ttCount++;
                                if (sem2_info.ElementAt(rns2).Value.Priority == 1
                                    &&
                                    sem2_info.ElementAt(rns2).Value.count < sem2_info.ElementAt(rns2).Value.SPW)
                                {
                                    sem2_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem2_info.ElementAt(rns2).Value.SubjectID,
                                        SubjectName = sem2_info.ElementAt(rns2).Value.SubjectName,
                                        ProfessorID = sem2_info.ElementAt(rns2).Value.ProfessorID,
                                        ProfessorName = sem2_info.ElementAt(rns2).Value.ProfessorName,
                                        SessionLocation = session_location(rns2, sem2_location, sem2_info)
                                    });
                                    sem2_info.ElementAt(rns2).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        if (gc > 2000000)
                        {
                            break;
                        }
                    }
                    if(sem2_tt.Count<25)
                    {
                        ViewBag.TryAgainS2 = "Issue generating Time Table for Semester 2";
                    }
                    ViewBag.Sem_2_TT = sem_n_final_tt(sem2_tt);

                    //Response.Write("<br/><br/><br/><br/>");
                    //for(int i=0;i<sem2_tt.Count;i++)
                    //{
                    //    Response.Write("<br/><br/>Key : " + sem2_tt.ElementAt(i).Key);
                    //    Response.Write("<br/>Value : " + sem2_tt.ElementAt(i).Value);
                    //}
                    //S2
                    //S4
                    gc = 0;
                    int s2ttCount = ttCount - 1;
                    ttCount = 1;
                    while (ttCount <= s2ttCount)
                    {
                        rns4 = Random_Number(sem4_info.Count);
                        if (ttCount > 3)
                        {
                            if (sem4_info.ElementAt(rns4).Value.count < sem4_info.ElementAt(rns4).Value.SPW
                                &&
                                sem2_tt[ttCount].ProfessorID != sem4_info.ElementAt(rns4).Value.ProfessorID
                                &&
                                sem4_tt[ttCount - 1].ProfessorID != sem4_info.ElementAt(rns4).Value.ProfessorID
                                &&
                                sem4_tt[ttCount - 2].ProfessorID != sem4_info.ElementAt(rns4).Value.ProfessorID)
                            {
                                sem4_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem4_info.ElementAt(rns4).Value.SubjectID,
                                    SubjectName = sem4_info.ElementAt(rns4).Value.SubjectName,
                                    ProfessorID = sem4_info.ElementAt(rns4).Value.ProfessorID,
                                    ProfessorName = sem4_info.ElementAt(rns4).Value.ProfessorName,
                                    SessionLocation = session_location(rns4, sem4_location, sem4_info)
                                });
                                sem4_info.ElementAt(rns4).Value.count++;
                                ttCount++;
                                if (ttCount > s2ttCount) { break; }
                                if (sem4_info.ElementAt(rns4).Value.count < sem4_info.ElementAt(rns4).Value.SPW
                                &&
                                sem2_tt[ttCount].ProfessorID != sem4_info.ElementAt(rns4).Value.ProfessorID)
                                {
                                    sem4_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem4_info.ElementAt(rns4).Value.SubjectID,
                                        SubjectName = sem4_info.ElementAt(rns4).Value.SubjectName,
                                        ProfessorID = sem4_info.ElementAt(rns4).Value.ProfessorID,
                                        ProfessorName = sem4_info.ElementAt(rns4).Value.ProfessorName,
                                        SessionLocation = session_location(rns4, sem4_location, sem4_info)
                                    });
                                    sem4_info.ElementAt(rns4).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        else
                        {
                            if (sem4_info.ElementAt(rns4).Value.count < sem4_info.ElementAt(rns4).Value.SPW
                                &&
                                sem2_tt[ttCount].ProfessorID != sem4_info.ElementAt(rns4).Value.ProfessorID)
                            {
                                sem4_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem4_info.ElementAt(rns4).Value.SubjectID,
                                    SubjectName = sem4_info.ElementAt(rns4).Value.SubjectName,
                                    ProfessorID = sem4_info.ElementAt(rns4).Value.ProfessorID,
                                    ProfessorName = sem4_info.ElementAt(rns4).Value.ProfessorName,
                                    SessionLocation = session_location(rns4, sem4_location, sem4_info)
                                });
                                sem4_info.ElementAt(rns4).Value.count++;
                                ttCount++;
                                if (ttCount > s2ttCount) { break; }
                                if (sem4_info.ElementAt(rns4).Value.count < sem4_info.ElementAt(rns4).Value.SPW
                                &&
                                sem2_tt[ttCount].ProfessorID != sem4_info.ElementAt(rns4).Value.ProfessorID)
                                {
                                    sem4_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem4_info.ElementAt(rns4).Value.SubjectID,
                                        SubjectName = sem4_info.ElementAt(rns4).Value.SubjectName,
                                        ProfessorID = sem4_info.ElementAt(rns4).Value.ProfessorID,
                                        ProfessorName = sem4_info.ElementAt(rns4).Value.ProfessorName,
                                        SessionLocation = session_location(rns4, sem4_location, sem4_info)
                                    });
                                    sem4_info.ElementAt(rns4).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        if (gc > 2000000)
                        {
                            break;
                        }
                    }
                    if(sem4_tt.Count<25)
                    {
                        ViewBag.TryAgainS4 = "Issue generating Time Table for Semester 4";
                    }
                    ViewBag.Sem_4_TT = sem_n_final_tt(sem4_tt);
                    //S4

                    //S1
                    ttCount = 1;
                    gc = 0;
                    while (ttCount <= 25)
                    {
                        rns1 = Random_Number(sem1_info.Count);
                        if (ttCount > 2)
                        {
                            if (sem1_info.ElementAt(rns1).Value.count < sem1_info.ElementAt(rns1).Value.SPW
                                &&
                                sem1_tt[ttCount - 1].ProfessorID != sem1_info.ElementAt(rns1).Value.ProfessorID
                                &&
                                sem1_tt[ttCount - 2].ProfessorID != sem1_info.ElementAt(rns1).Value.ProfessorID)
                            {
                                sem1_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem1_info.ElementAt(rns1).Value.SubjectID,
                                    SubjectName = sem1_info.ElementAt(rns1).Value.SubjectName,
                                    ProfessorID = sem1_info.ElementAt(rns1).Value.ProfessorID,
                                    ProfessorName = sem1_info.ElementAt(rns1).Value.ProfessorName,
                                    SessionLocation = session_location(rns1, sem1_location, sem1_info)
                                });
                                sem1_info.ElementAt(rns1).Value.count++;
                                ttCount++;
                                if (sem1_info.ElementAt(rns1).Value.Priority == 1
                                    &&
                                    sem1_info.ElementAt(rns1).Value.count < sem1_info.ElementAt(rns1).Value.SPW)
                                {
                                    sem1_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem1_info.ElementAt(rns1).Value.SubjectID,
                                        SubjectName = sem1_info.ElementAt(rns1).Value.SubjectName,
                                        ProfessorID = sem1_info.ElementAt(rns1).Value.ProfessorID,
                                        ProfessorName = sem1_info.ElementAt(rns1).Value.ProfessorName,
                                        SessionLocation = session_location(rns1, sem1_location, sem1_info)
                                    });
                                    sem1_info.ElementAt(rns1).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        else
                        {
                            if (sem1_info.ElementAt(rns1).Value.count < sem1_info.ElementAt(rns1).Value.SPW)
                            {
                                sem1_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem1_info.ElementAt(rns1).Value.SubjectID,
                                    SubjectName = sem1_info.ElementAt(rns1).Value.SubjectName,
                                    ProfessorID = sem1_info.ElementAt(rns1).Value.ProfessorID,
                                    ProfessorName = sem1_info.ElementAt(rns1).Value.ProfessorName,
                                    SessionLocation = session_location(rns1, sem1_location, sem1_info)
                                });
                                sem1_info.ElementAt(rns1).Value.count++;
                                ttCount++;
                                if (sem1_info.ElementAt(rns1).Value.Priority == 1
                                    &&
                                    sem1_info.ElementAt(rns1).Value.count < sem1_info.ElementAt(rns1).Value.SPW)
                                {
                                    sem1_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem1_info.ElementAt(rns1).Value.SubjectID,
                                        SubjectName = sem1_info.ElementAt(rns1).Value.SubjectName,
                                        ProfessorID = sem1_info.ElementAt(rns1).Value.ProfessorID,
                                        ProfessorName = sem1_info.ElementAt(rns1).Value.ProfessorName,
                                        SessionLocation = session_location(rns1, sem1_location, sem1_info)
                                    });
                                    sem1_info.ElementAt(rns1).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        if (gc > 2000000)
                        {
                            ViewBag.TryAgainS1 = "Issue generating Time Table for Semester 1";
                            break;
                        }
                    }
                    ViewBag.Sem_1_TT = sem_n_final_tt(sem1_tt);
                    //S1
                    //S3
                    gc = 0;
                    int s1ttCount = ttCount - 1;
                    ttCount = 1;
                    while (ttCount <= s1ttCount)
                    {
                        rns3 = Random_Number(sem3_info.Count);
                        if (ttCount > 3)
                        {
                            if (sem3_info.ElementAt(rns3).Value.count < sem3_info.ElementAt(rns3).Value.SPW
                                &&
                                sem1_tt[ttCount].ProfessorID != sem3_info.ElementAt(rns3).Value.ProfessorID
                                &&
                                sem3_tt[ttCount - 1].ProfessorID != sem3_info.ElementAt(rns3).Value.ProfessorID
                                &&
                                sem3_tt[ttCount - 2].ProfessorID != sem3_info.ElementAt(rns3).Value.ProfessorID)
                            {
                                sem3_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem3_info.ElementAt(rns3).Value.SubjectID,
                                    SubjectName = sem3_info.ElementAt(rns3).Value.SubjectName,
                                    ProfessorID = sem3_info.ElementAt(rns3).Value.ProfessorID,
                                    ProfessorName = sem3_info.ElementAt(rns3).Value.ProfessorName,
                                    SessionLocation = session_location(rns3, sem3_location, sem3_info)
                                });
                                sem3_info.ElementAt(rns3).Value.count++;
                                ttCount++;
                                if (ttCount > s1ttCount) { break; }
                                if (sem3_info.ElementAt(rns3).Value.count < sem3_info.ElementAt(rns3).Value.SPW
                                &&
                                sem1_tt[ttCount].ProfessorID != sem3_info.ElementAt(rns3).Value.ProfessorID)
                                {
                                    sem3_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem3_info.ElementAt(rns3).Value.SubjectID,
                                        SubjectName = sem3_info.ElementAt(rns3).Value.SubjectName,
                                        ProfessorID = sem3_info.ElementAt(rns3).Value.ProfessorID,
                                        ProfessorName = sem3_info.ElementAt(rns3).Value.ProfessorName,
                                        SessionLocation = session_location(rns3, sem3_location, sem3_info)
                                    });
                                    sem3_info.ElementAt(rns3).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        else
                        {
                            if (sem3_info.ElementAt(rns3).Value.count < sem3_info.ElementAt(rns3).Value.SPW
                                &&
                                sem1_tt[ttCount].ProfessorID != sem3_info.ElementAt(rns3).Value.ProfessorID)
                            {
                                sem3_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem3_info.ElementAt(rns3).Value.SubjectID,
                                    SubjectName = sem3_info.ElementAt(rns3).Value.SubjectName,
                                    ProfessorID = sem3_info.ElementAt(rns3).Value.ProfessorID,
                                    ProfessorName = sem3_info.ElementAt(rns3).Value.ProfessorName,
                                    SessionLocation = session_location(rns3, sem3_location, sem3_info)
                                });
                                sem3_info.ElementAt(rns3).Value.count++;
                                ttCount++;
                                if (ttCount > s1ttCount) { break; }
                                if (sem3_info.ElementAt(rns3).Value.count < sem3_info.ElementAt(rns3).Value.SPW
                                &&
                                sem1_tt[ttCount].ProfessorID != sem3_info.ElementAt(rns3).Value.ProfessorID)
                                {
                                    sem3_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem3_info.ElementAt(rns3).Value.SubjectID,
                                        SubjectName = sem3_info.ElementAt(rns3).Value.SubjectName,
                                        ProfessorID = sem3_info.ElementAt(rns3).Value.ProfessorID,
                                        ProfessorName = sem3_info.ElementAt(rns3).Value.ProfessorName,
                                        SessionLocation = session_location(rns3, sem3_location, sem3_info)
                                    });
                                    sem3_info.ElementAt(rns3).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        if (gc > 2000000)
                        {
                            break;
                        }
                    }
                    if(sem3_tt.Count<25)
                    {
                        ViewBag.TryAgainS3 = "Issue generating Time Table for Semester 3";
                    }
                    ViewBag.Sem_3_TT = sem_n_final_tt(sem3_tt);
                    //S3

                    //S5
                    gc = 0;
                    int s3ttCount = ttCount - 1;
                    ttCount = 1;
                    while (ttCount <= s3ttCount)
                    {
                        rns5 = Random_Number(sem5_info.Count);
                        if (ttCount > 3)
                        {
                            if (sem5_info.ElementAt(rns5).Value.count < sem5_info.ElementAt(rns5).Value.SPW
                                &&
                                sem1_tt[ttCount].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID
                                &&
                                sem3_tt[ttCount].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID
                                &&
                                sem3_tt[ttCount - 1].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID
                                &&
                                sem3_tt[ttCount - 2].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID)
                            {
                                sem5_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem5_info.ElementAt(rns5).Value.SubjectID,
                                    SubjectName = sem5_info.ElementAt(rns5).Value.SubjectName,
                                    ProfessorID = sem5_info.ElementAt(rns5).Value.ProfessorID,
                                    ProfessorName = sem5_info.ElementAt(rns5).Value.ProfessorName,
                                    SessionLocation = session_location(rns5, sem5_location, sem5_info)
                                });
                                sem5_info.ElementAt(rns5).Value.count++;
                                ttCount++;
                                if (ttCount > s3ttCount) { break; }
                                if (sem5_info.ElementAt(rns5).Value.count < sem5_info.ElementAt(rns5).Value.SPW
                                    &&
                                sem3_tt[ttCount].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID
                                    &&
                                sem1_tt[ttCount].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID)
                                {
                                    sem5_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem5_info.ElementAt(rns5).Value.SubjectID,
                                        SubjectName = sem5_info.ElementAt(rns5).Value.SubjectName,
                                        ProfessorID = sem5_info.ElementAt(rns5).Value.ProfessorID,
                                        ProfessorName = sem5_info.ElementAt(rns5).Value.ProfessorName,
                                        SessionLocation = session_location(rns5, sem5_location, sem5_info)
                                    });
                                    sem5_info.ElementAt(rns5).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        else
                        {
                            if (sem5_info.ElementAt(rns5).Value.count < sem5_info.ElementAt(rns5).Value.SPW
                                &&
                                sem3_tt[ttCount].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID
                                &&
                                sem1_tt[ttCount].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID)
                            {
                                sem5_tt.Add(ttCount, new Result_Data
                                {
                                    SubjectID = sem5_info.ElementAt(rns5).Value.SubjectID,
                                    SubjectName = sem5_info.ElementAt(rns5).Value.SubjectName,
                                    ProfessorID = sem5_info.ElementAt(rns5).Value.ProfessorID,
                                    ProfessorName = sem5_info.ElementAt(rns5).Value.ProfessorName,
                                    SessionLocation = session_location(rns5, sem5_location, sem5_info)
                                });
                                sem5_info.ElementAt(rns5).Value.count++;
                                ttCount++;
                                if (ttCount > s3ttCount) { break; }
                                if (sem5_info.ElementAt(rns5).Value.count < sem5_info.ElementAt(rns5).Value.SPW
                                    &&
                                    sem3_tt[ttCount].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID
                                &&
                                sem1_tt[ttCount].ProfessorID != sem5_info.ElementAt(rns5).Value.ProfessorID)
                                {
                                    sem5_tt.Add(ttCount, new Result_Data
                                    {
                                        SubjectID = sem5_info.ElementAt(rns5).Value.SubjectID,
                                        SubjectName = sem5_info.ElementAt(rns5).Value.SubjectName,
                                        ProfessorID = sem5_info.ElementAt(rns5).Value.ProfessorID,
                                        ProfessorName = sem5_info.ElementAt(rns5).Value.ProfessorName,
                                        SessionLocation = session_location(rns5, sem5_location, sem5_info)
                                    });
                                    sem5_info.ElementAt(rns5).Value.count++;
                                    ttCount++;
                                }
                            }
                            else
                            {
                                gc++;
                            }
                        }
                        if (gc > 2000000)
                        {
                            break;
                        }
                    }
                    if(sem5_tt.Count<25)
                    {
                        ViewBag.TryAgainS5 = "Issue generating Time Table for Semester 5";
                    }
                    ViewBag.Sem_5_TT = sem_n_final_tt(sem5_tt);
                    //S5
                }
                else
                {
                    if (s1tspw != 25)
                    {
                        ViewData["Sem1TS"] = "Semester 1 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s1tspw;
                    }
                    if (s3tspw != 25)
                    {
                        ViewData["Sem3TS"] = "Semester 3 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s3tspw;
                    }
                    if (s5tspw != 25)
                    {
                        ViewData["Sem5TS"] = "Semester 5 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s5tspw;
                    }
                    if (s2tspw != 25)
                    {
                        ViewData["Sem2TS"] = "Semester 2 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s2tspw;
                    }
                    if (s4tspw != 25)
                    {
                        ViewData["Sem4TS"] = "Semester 4 Total Session Per Week not equals to 25\nTotal Session Per Week : " + s4tspw;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        private int temp;
        public int Random_Number(int sem_SPW)
        {
            Random num = new Random();
            int rn;
            while (true)
            {
                rn = num.Next(0, sem_SPW);
                if (rn == temp) { continue; }
                else { break; }
            }
            temp = rn;
            return rn;
            //return num.Next(0,sem_SPW);
        }
        //TTG

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string id)
        {
            //Response.Write(Convert.ToString(Request.Form["userID"]));
            //Response.Write("\n" + Convert.ToString(Request.Form["password"]));
            String uid = Convert.ToString(Request.Form["userID"]);
            String pw = Convert.ToString(Request.Form["password"]);
            if(uid.Equals("Admin") && pw.Equals("admin@ttg"))
            {
                Session["userAdmin"] = uid;
                return RedirectToAction("DashBoard");
            }
            else
            {
                ViewBag.ErrorMsg = "Invalid Credentials";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            if(Session["userAdmin"]!=null)
            {
                Session.Clear();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("DashBoard");
            }
        }
    }
}