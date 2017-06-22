ExerCize is a MVC portfolio project that logs user workouts and calculates how many calories are burned during each activity. The app was created to solve a set of common web development problems including setting up admin and user roles, querying user information from a database, and displaying the queried information visually. The techniques to solve these problems are described in detail below.

## Libraries and Resources Used 

- [Chart.Mvc](https://github.com/martinobordin/Chart.Mvc) - A .NET wrapper to generate charts using the popular Chart.Js V1 library (http://www.chartjs.org).
- [SYEDSHAUNU](https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97) - Great Tutorial on setting up an admin user role written by a Microsoft MVP.
- [Scott Allen - LINQ Fundamentals](https://app.pluralsight.com/library/courses/linq-fundamentals-csharp-6/table-of-contents) - Great course on querying a database using LINQ
- [Calorie Equation](https://www.hss.edu/conditions_burning-calories-with-exercise-calculating-estimated-energy-expenditure.asp) - This was the study I used to calculate the calories burned with each workout


## How to use this Application on the web.

1. Launch the [App](http://exercize-env.us-east-1.elasticbeanstalk.com/).
 
2. Click Login. 

3. The application runs differently depending on whether you log in as an admin or user. Login credentials for both are on the right side of login page

4. If logged in as an admin select View Customer Data, if logged in as a user try to create a Workout or view Progress!


## How to run locally

1. Clone the repo 

2. Open the project and set up Home/Index as the start up file in not already

3. Start running the application. 

4. Click Login

5. The application runs differently depending on whether you log in as an admin or user. Login credentials for both are on the right side of login page

6. In order for the administrator to have customer data to view, a customer and their workouts need to be created. 

## Index

* [Setting up an Admin Role](#Admin)
* [Creating a User Workout](#workouts)
* [Creating Charts](#charts)

---

## Admin

[demo](admin-role) 

Since this application behaves differently if you are logged in as the Admininstrator it is important to see how to implement this utilizing the already in place user authentification services from the .NET Identity FrameWork. In order to utilize the boilerplate code when creating your MVC project make sure to select "Single User Authenticication" when promted during setup.

1. To create and populate an admin role, you need to add this to the Startup.cs file
    
```cs
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            if (!roleManager.RoleExists("Admin"))
            {
                //Creates Admin roll
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Populate Admin User
                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";

                string userPWD = "Password1!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
              }
        }
```

2. Once the admin has been created you can then change change how their home page is presented. This is done by utilizing ViewBags. In the HomeController add the following


```cs

        public ActionResult Index()
                {
                    if(!User.Identity.IsAuthenticated)
                    {
                        ViewBag.displayMenu = "Login";
                    }

                    if (User.Identity.IsAuthenticated)
                    {
                        var user = User.Identity;
                        ViewBag.Name = user.Name;

                        ViewBag.displayMenu = "No";

                        if (isAdminUser())
                        {
                            ViewBag.displayMenu = "Yes";                    
                        }
                        return View();
                    }
                    else
                    {
                        ViewBag.Name = "Not Logged IN";
                    }
                    return View();

                }

                public Boolean isAdminUser()
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var user = User.Identity;
                        ApplicationDbContext context = new ApplicationDbContext();
                        var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                        var p = usermanager.GetEmail(user.GetUserId());
                        if (p != "admin@admin.com")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    return false;
                }        
```

3. Once the ViewBags have been set, implement them in the Home/Index View as shown

```cs
        <div class="banner">
            <div class="bg-color">
                <div class="container">
                    <div class="row">
                        <div class="banner-text text-center col-sm-12">
                            <div class="text-border">
                                <h2 class="text-dec">Exercize</h2>
                            </div>
                            <div class="intro-para text-center quote">
                                
                                    @if (ViewBag.displayMenu == "Yes")
                                {
                                <p>Welcome Admin.</p>
                                
                                    <li>@Html.ActionLink("View Customer Data", "CustomerData", "Admin", new { }, new { @class = "btn" })</li>
                                
                                }
                                    else if (ViewBag.displayMenu == "No")
                                    {
                                        <p>Welcome User</p>
                                        <p>
                                            <li>@Html.ActionLink("Go To Workouts", "Index", "Exercise", new { }, new { @class = "btn" })</li>
                                        </p>
                                    }
                                    else
                                    {
                                        <li>@Html.ActionLink("Login", "Login", "Account", new { }, new { @class = "btn" })</li>
                                                                
                                    } </p>                        
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
```
Now depending on the login information your home view and action options are modified, great!

## Workouts 
[demo](#user-workout)

The next problem of this project was having the ability as a user to create a workout which calculates an estimatation of burned calories. 
I found the calculation for this [here](https://www.hss.edu/conditions_burning-calories-with-exercise-calculating-estimated-energy-expenditure.asp)

**Energy expenditure (calories/minute) = .0175 x Activity (from table) x weight (in kilograms)**

1.To cacluate this for each user we must have them enter in their weight during registration. To do this I added the following to the RegisterViewModel

```cs
        public class RegisterViewModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            
            public string Sex { get; set; }

           
            public double Age { get; set; } 

            [Required]
            public double Weight { get; set; }
        }
```

2. And this to the Register View

```cs
        @using (Html.BeginForm("Register", "Account", FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
        {
            @Html.AntiForgeryToken()
            <h4>Create a new account.</h4>
            <hr />
            @Html.ValidationSummary("", new { @class = "text-danger" })
            <div class="form-group">
                @Html.LabelFor(m => m.Email, new { @class = "col-md-2 control-label" })
                <div class="col-md-10">
                    @Html.TextBoxFor(m => m.Email, new { @class = "form-control" })
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(m => m.Password, new { @class = "col-md-2 control-label" })
                <div class="col-md-10">
                    @Html.PasswordFor(m => m.Password, new { @class = "form-control" })
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(m => m.ConfirmPassword, new { @class = "col-md-2 control-label" })
                <div class="col-md-10">
                    @Html.PasswordFor(m => m.ConfirmPassword, new { @class = "form-control" })
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(m => m.Age, new { @class = "col-md-2 control-label" })
                <div class="col-md-10">
                    @Html.TextBoxFor(m => m.Age, new { @class = "form-control" })
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(m => m.Sex, new { @class = "col-md-2 control-label" })
                <div class="col-md-10">
                    @Html.TextBoxFor(m => m.Sex, new { @class = "form-control" })
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(m => m.Weight, new { @class = "col-md-2 control-label" })
                <div class="col-md-10">
                    @Html.TextBoxFor(m => m.Weight, new { @class = "form-control" })
                </div>
            </div>
```

3. This ensures that each user will have their assoicated weight in the database. The next thing we must do is retreive that weight each time we are creating a new workout. This is done in the Exercise Controller by creating a new UserManager instance that contains all the database row infomation of the currently logged in user. 

```cs

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExerciseCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());        
            var userWeight = currentUser.Weight;

            var service = new ExerciseService(userId, userWeight);
            service.CreateExercise(model);

            return RedirectToAction("Index");            
        }
```
4. Once the weight is retrieved we pass it into our ExerciseService to create the workout using the constructor that takes the userId and userWeightInPounds.

```cs

        public class ExerciseService
            {
                private readonly Guid _userId;
                private readonly Double _userWeightInPounds;

                public ExerciseService(Guid userId, Double userWeightInPounds)
                {
                    _userId = userId;
                    _userWeightInPounds = userWeightInPounds;
                }

                public ExerciseService(Guid userId)
                {
                    _userId = userId;
                 
                }

                public bool CreateExercise(ExerciseCreate model)
                {
                    var entity =
                        new Workout()
                        {
                            OwnerId = _userId,
                            OwnerWeight = _userWeightInPounds,
                            Type = model.Type,
                            Intensity = model.Intensity,
                            Duration = model.Duration,
                            CaloriesBurned = 0,
                            CreatedUtc = DateTimeOffset.UtcNow,
                        };

                    
                    CalorieCalculator.GetCalories(entity);
           
                    using (var ctx = new ApplicationDbContext())
                    {
                        ctx.Workouts.Add(entity);                
                        return ctx.SaveChanges() == 1;
                    }

                   
                }

``` 
5. Notice here the helper method CalorieCalculator.GetCalorites(entity).

```cs

        namespace Exercise.Services.HelperMethods

        {
            public class CalorieCalculator
            {
                public static double GetCalories(Workout entity)
                {

                    if (entity.Type == "Bicycling" && entity.Intensity == "Low")
                    {
                        entity.CaloriesBurned = entity.Duration * .0175 * 6.0 * (entity.OwnerWeight / 2.2);
                        entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
                        return entity.CaloriesBurned;
                    }
                    else if (entity.Type == "Bicycling" && entity.Intensity == "High")
                    {
                        entity.CaloriesBurned = entity.Duration * .0175 * 10 * (entity.OwnerWeight / 2.2);
                        entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
                        return entity.CaloriesBurned;
                    }
                    else if (entity.Type == "Dancing" && entity.Intensity == "Low")
                    {
                        entity.CaloriesBurned = entity.Duration * .0175 * 5.0 * (entity.OwnerWeight / 2.2);
                        entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
                        return entity.CaloriesBurned;
                    }
                    else if (entity.Type == "Dancing" && entity.Intensity == "High")
                    {
                        entity.CaloriesBurned = entity.Duration * .0175 * 7.0 * (entity.OwnerWeight / 2.2);
                        entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
                        return entity.CaloriesBurned;
                    }
                    else if (entity.Type == "Running" && entity.Intensity == "Low")
                    {
                        entity.CaloriesBurned = entity.Duration * .0175 * 10.0 * (entity.OwnerWeight / 2.2);
                        entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
                        return entity.CaloriesBurned;
                    }
                    else if (entity.Type == "Running" && entity.Intensity == "High")
                    {
                        entity.CaloriesBurned = entity.Duration * .0175 * 13.0 * (entity.OwnerWeight / 2.2);
                        entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
                        return entity.CaloriesBurned;
                    }
                    else if (entity.Type == "Swimming" && entity.Intensity == "Low")
                    {
                        entity.CaloriesBurned = entity.Duration * .0175 * 6 * (entity.OwnerWeight / 2.2);
                        entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
                        return entity.CaloriesBurned;
                    }
                    else if (entity.Type == "Swimming" && entity.Intensity == "High")
                    {
                        entity.CaloriesBurned = entity.Duration * .0175 * 10.0 * (entity.OwnerWeight / 2.2);
                        entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
                        return entity.CaloriesBurned;
                    }
                    else if (entity.Type == "Walking" && entity.Intensity == "Low")
                    {
                        entity.CaloriesBurned = entity.Duration * .0175 * 3.5 * (entity.OwnerWeight / 2.2);
                        entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
                        return entity.CaloriesBurned;
                    }
                    else if (entity.Type == "Walking" && entity.Intensity == "High")
                    {
                        entity.CaloriesBurned = entity.Duration * .0175 * 5.0 * (entity.OwnerWeight / 2.2);
                        entity.CaloriesBurned = Math.Round(entity.CaloriesBurned, 2);
                        return entity.CaloriesBurned;
                    }
                    else
                    {
                        return entity.CaloriesBurned;
                    }
                }

            }
        }

```
This is taking the available types of activies, along with intensity and user wight to calculate how many calories are burned. Notice it is dividing the OwnerWeight by 2.2. This is convert it to kilograms which is the variable used in the study's formula. 

6. Thats it! Now for each new workout created by any user, a calorie burned calculation is taking place, stored to the database and displayed in their list of work outs. 
            
## Charts
[demo](#charts)

The last challenge of this application was to visually represent data for both the user and admin roles. This was done by implementing Chart.MVC, a great, if a bit outdated, extenstion that makes working with chart.js a bit more C# friendly. Using it I was able to create bar, line and radial graphs that represented different database query results. 

1. The first set of these queries was to show each user a line graph of their workouts with how many calories were burned of each. Notice these are LINQ queries. Here is what I added to my Exercise Controller. 

```cs

        public ActionResult Progress()
                {
                    var userId = Guid.Parse(User.Identity.GetUserId());
                    var service = new ExerciseService(userId);            

                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var currentUser = manager.FindById(User.Identity.GetUserId());

                    using (var context = new ApplicationDbContext())
                    {

                        var query = from b in context.Workouts
                                    where b.OwnerId == userId
                                    select b.CaloriesBurned;
                        List<double> plist = query.ToList();
                        ViewBag.caloriesBurned = plist;

                        var query2 = from b in context.Workouts
                                     where b.OwnerId == userId
                                     select b.Type;
                        List<string> elist = query2.ToList();
                        ViewBag.type = elist;
                    }


                    return View();
                }
```

2. With the type of workouts and how many calories were burned in each stored in their respective ViewBags, I next tied that to the Progress View as shown here. Notice the using statements as this is how Chart.Mvc is implemented

```cs
        @model Exercise.Models.ExerciseDetail

        @using Chart.Mvc.ComplexChart;
        @using Chart.Mvc.Extensions
        @Scripts.Render("~/bundles/chart.js")

        <h2>Progress</h2>

        @{
            var barChart = new LineChart();
            barChart.ComplexData.Labels.AddRange(ViewBag.type);
            barChart.ComplexData.Datasets.AddRange(new List<ComplexDataset>
                                     {
                                        
                                        new ComplexDataset
                                            {
                                                Data = ViewBag.caloriesBurned,
                                                Label = "My Second dataset",
                                                FillColor = "rgba(228, 247, 187, 1)",
                                                StrokeColor = "rgba(151,187,205,1)",
                                                PointColor = "rgba(151,187,205,1)",
                                                PointStrokeColor = "#fff",
                                                PointHighlightFill = "#fff",
                                                PointHighlightStroke = "rgba(151,187,205,1)",
                                            }
                                    });
        }

        <canvas id="myCanvas" width="940" height="400"></canvas>
        @Html.CreateChart("myCanvas", barChart)
```

3. You should now see a chart with these values. Here is what a sample one of mine looks like. 
[![UserChart.jpg](https://s17.postimg.org/l9ouz19e7/User_Chart.jpg)](https://postimg.org/image/l9ouz19e3/)

4. The next query was used to show the admin how many times each category of activity was created for all users as a radar chart. Here is what I added to my Admin Controller. Notice that this is a SQL query. 

```cs

        public ActionResult CustomerData()
                {
                    using (var context = new ApplicationDbContext())
                    {                
                                               
                        var ListTypeOfWorkouts = context.Database.SqlQuery<string>("SELECT Type FROM dbo.Workout").ToList();
                        
                        double walkCount = 0;
                        double runCount = 0;
                        double bikeCount = 0;
                        double danceCount = 0;
                        double swimCount = 0;
                        double maleCount = 0;
                        double femaleCount = 0;

```
5. Once the query is complete it stores all the results in a ListTypeOfWorkouts variable. I then created a for each loop to parse through the list and store each category of activity in its own variable. And finally set each category variable to a viewbag.

```cs

        foreach (var item in ListTypeOfWorkouts)
        {
            if (item == "Walking")
            {
                walkCount++;
            }
            else if (item == "Running")
            {
                runCount++;
            }
            else if (item == "Bicycling")
            {
                bikeCount++;
            }
            else if (item == "Dancing")
            {
                danceCount++;
            }
            else if (item == "Swimming")
            {
                swimCount++;
            }
        }

        ViewBag.WalkStat = walkCount;
        ViewBag.RunStat = runCount;
        ViewBag.BikeStat = bikeCount;
        ViewBag.DanceStat = danceCount;
        ViewBag.SwimStat = swimCount;
        ViewBag.MaleCount = maleCount;
        ViewBag.FemaleCount = femaleCount;
        ViewBag.TotalCalories = TotalCaloriesSum.ToString();
    }            
    return View();
```
6. With this information in the ViewBags we can then implement them into the view and construct the radar graph. Injecting the viewbags into the data list field.

```cs

        @model Exercise.Models.ExerciseDetail


        @using Chart.Mvc.ComplexChart
        @using Chart.Mvc.Extensions
        @using Chart.Mvc.SimpleChart


        @Scripts.Render("~/bundles/Chart")
        @Scripts.Render("~/bundles/jquery")
        <link href="/Content/Site.css" rel="stylesheet" type="text/css" />

        
            @{
                const string Canvas = "RadarChart";
                var complexChart = new RadarChart();
                complexChart.ComplexData.Labels.AddRange(new[] { "Walking", "Running", "Bicycling", "Dancing", "Swimming" });
                complexChart.ComplexData.Datasets.AddRange(new List<ComplexDataset>
            {


                  new ComplexDataset
                  {
                      Data = new List<double> { ViewBag.WalkStat, ViewBag.RunStat, ViewBag.RunStat, ViewBag.DanceStat, ViewBag.SwimStat },
                      Label = "My Second dataset",
                      FillColor = "rgba(228, 247, 187, 1)",
                      StrokeColor = "rgba(151,187,205,1)",
                      PointColor = "rgba(151,187,205,1)",
                      PointStrokeColor = "#fff",
                      PointHighlightFill = "#fff",
                      PointHighlightStroke = "rgba(151,187,205,1)",
                  }

            });
            }
            
            <canvas id="@Canvas" width="350" height="400"></canvas>
            @Html.CreateChart(Canvas, complexChart)
            </div>
        </div>
```
7. You should now see a radar chart, here is an example chart of for my admin
[![AdminChart.jpg](https://s22.postimg.org/g43zh8j0x/Admin_Chart.jpg)](https://postimg.org/image/5ha6btavh/)

## Conclusion

I hope you found this helpful, you can email me with any questions at ben.micah.peterson@gmail.com