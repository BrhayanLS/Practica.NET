namespace LinqSnnipets
{
    public class Snnipets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VM Golf",
                "VM California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat Leon"
            };
            //1. Select * of cars
            var carList = from car in cars select car;
            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            //2. Select Where
            var audiList = from car in cars where car.Contains("Audi") select car;
            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }
        }

        //Numbers Examples
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //Todos * 3, menos el 9, y ascendente
            var processedNumberList = numbers
                .Select(num => num * 3)
                .Where(num => num != 9)
                .OrderBy(num => num);
        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a", "bx", "c", "d", "e", "cj", "f", "c"
            };

            //1. Primero de todos
            var first = textList.First();

            //2. primero en ser c
            var cText = textList.First(text => text.Equals("c"));

            //3. primero en contener c
            var jText = textList.First(text => text.Contains("j"));

            //4. primer elemnto z o valor por defecto
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z"));

            //5. ultimo elemnto z o valor por defecto
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z"));

            //6. Valor unico
            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            int[] eventNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEventNumbers = { 0, 2, 6 };

            //Obtener 4 , 8
            var myEventNumbers = eventNumbers.Except(otherEventNumbers);
        }

        static public void MultipleSelects()
        {
            //Select Many
            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3"
            };
            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id= 1,
                    Name="Enterprise 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id=1,
                            Name="Martin",
                            Email="martin@mail.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id=2,
                            Name="Pepe",
                            Email="pepe@mail.com",
                            Salary = 2000
                        },
                        new Employee
                        {
                            Id=3,
                            Name="Juan",
                            Email="juan@mail.com",
                            Salary = 2500
                        }
                    }
                },
                new Enterprise()
                {
                    Id= 2,
                    Name="Enterprise 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id=4,
                            Name="ana",
                            Email="ana@mail.com",
                            Salary = 3500
                        },
                        new Employee
                        {
                            Id=5,
                            Name="Maria",
                            Email="maria@mail.com",
                            Salary = 2500
                        },
                        new Employee
                        {
                            Id=6,
                            Name="Martha",
                            Email="martha@mail.com",
                            Salary = 4000
                        }
                    }
                }
            };

            //All empleados de all empresas
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Saber si es una lsita vacia
            bool hasEnterprises = enterprises.Any();
            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            //All empresas empleados con mas de 1000 de salario
            bool hasEmployeeWithSalaryMoreThan1000 =
                enterprises.Any(entreprise =>
                    entreprise.Employees.Any(employee => employee.Salary >= 1000));
        }

        static public void linqCollections()
        {
            var firtsList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            //Innerjoin
            var commonResult = from element in firtsList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firtsList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element, secondElement) => new { element, secondElement }
                );

            //outer join - left
            var leftOuterJoin = from element in firtsList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firtsList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, secondElement = secondElement };

            //outer join - right
            var rightOuterJoin = from secondElement in secondList
                                 join element in firtsList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            //union
            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            var skipTwoFirstValue = myList.Skip(2); //{3,4,5,6,7,8,9,10}
            var skipTwoLastValue = myList.SkipLast(2); //{1,2,3,4,5,6,7,8}
            var skipWhile = myList.SkipWhile(s => s < 4); //{4,5,6,7,89,10}

            var takeFirstTwoValues = myList.Take(2); //{1,2}
            var takeLastTwoValues = myList.TakeLast(2); //{9,10}
            var takeWhileSmallerThan4 = myList.TakeWhile(s => s < 4); //{1,2,3,4}
        }

        //pagging with skip - take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultPerPage)
        {
            int startIndex = (pageNumber - 1) * resultPerPage;
            return collection.Skip(startIndex).Take(resultPerPage);
        }

        //Variables
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 8, 9, 10 };
            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;
            Console.WriteLine("Average: {0}", numbers.Average());
            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number: {0} Square: {1}", number, Math.Pow(number, 2));
            }
        }

        //ZIP
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6 };
            string[] stringNumbers = { "one", "two", "three", "four", "five", "six" };
            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + " = " + word);
        }

        //Repeat & Range
        static public void repeatRangeLinq()
        {
            //Generete collection from 1 - 1000 -->Range
            IEnumerable<int> first1000 = Enumerable.Range(1, 1000);

            //Repeat a value N times
            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); //{"X","X","X","X","X"}
        }

        static public void studentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martin",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Ana",
                    Grade = 96,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Alvaro",
                    Grade = 10,
                    Certified = false,
                },
                new Student
                {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true,
                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var noCertifiedStudents = from student in classRoom
                                      where student.Certified == false
                                      select student;

            var aprovedStudentNames = from student in classRoom
                                      where student.Certified == true && student.Grade >= 50
                                      select student.Name;
        }

        //All
        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };
            bool allAreSmallerThan10 = numbers.All(x => x < 10);//true
            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2);//false
            bool oneAreBiggerOrEqualThan2 = numbers.Any(x => x >= 2);//true

            var emptyList = new List<int>();
            bool allNumbersAreGreaterThan0 = numbers.All(x => x >= 0);//true
        }

        //aggregate
        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            //0,1 = 1
            //1,2 = 3
            //3,3 = 6....

            string[] words = { "Hello, ", "my ", "name ", "is ", "Martin" };
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);
        }

        //Distinc
        static public void distincValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> distincValues = numbers.Distinct();
        }

        //GroupBy
        static public void groupByExample()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            //we will have two groups
            //1. los que no cumplen la condicion
            //2. los que si cumplen la condicion

            foreach (var group in grouped )
            {
                foreach (var item in group)
                {
                    Console.WriteLine(item);//1,3,5,7,9...2,4,6,8
                }
            }

            //Another exaple
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martin",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Ana",
                    Grade = 96,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Alvaro",
                    Grade = 10,
                    Certified = false,
                },
                new Student
                {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true,
                }
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified);
            //Obtenemos dos grupos
            //1. No certificados
            //2. certificados

            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("---------------{0}--------------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }
        }

        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Commets = new List<Commet>()
                    {
                        new Commet()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My First Comment",
                            Content = "My content"
                        },
                        new Commet()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My Second Comment",
                            Content = "My other content"
                        },
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My Second post",
                    Content = "My Second content",
                    Created = DateTime.Now,
                    Commets = new List<Commet>()
                    {
                        new Commet()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My other Comment",
                            Content = "My new content"
                        },
                        new Commet()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "My Second Comment",
                            Content = "My other content"
                        },
                    }
                }
            };

            var commentsWithContent = posts.SelectMany(
                post => post.Commets,
                    (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });
        }
    }
}
