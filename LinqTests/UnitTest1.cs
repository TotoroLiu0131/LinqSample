using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void employ_test()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyWhere(x => x.Age >= 25 && x.Age <= 40).ToList();

            var expected = new List<Employee>()
            {
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6} ,
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6},
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = WithoutLinq.FindProductByPriceAndSupplier(products, 200, 500, "Odd-e");

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" },
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void LINQ_where()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.Where(p => p.Price >= 200 && p.Price <= 500 && p.Supplier == "Odd-e").ToList();
            //var actual = YourOwnLinq.MyWhere(source, p => p.Price >= 200 && p.Price <= 500 && p.Supplier == "Odd-e").ToList();

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" },
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void test_employees()
        {
            var urls = RepositoryFactory.GetEmployees();
            var actual = urls.MyWhere(x => x.Role == RoleType.Engineer).MySelect(x => x.MonthSalary);

            var expected = new List<int>()
            {
                100,140,280,120,250
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void test_employees_with_index()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyTake(3).MySelect((x, i) => ($"{i + 1}-{x.Name}"));

            var expected = new List<string>()
            {
                "1-Frank","2-Andy","3-Mary"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void test_employees_skip()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MySkip(6);

            var expected = new List<Employee>()
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void test_products_skip()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MySkipWhile(4, x => x.Price > 300);

            var expected = new List<Product>()
            {
                new Product{Id=1, Cost=11, Price=110, Supplier="Odd-e" },
                new Product{Id=2, Cost=21, Price=210, Supplier="Yahoo" },
                new Product{Id=7, Cost=71, Price=710, Supplier="Yahoo" },
                new Product{Id=8, Cost=18, Price=780, Supplier="Yahoo" },
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void test_products_takeWhile()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyRealTakeWhile(x => x.MonthSalary < 200);

            var expected = new List<Employee>()
            {
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Andy", Role=RoleType.OP, MonthSalary=80, Age=22, WorkingYear=2.6} ,
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6} ,
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void group_sum_products()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyGroupSum(3, x => x.Price);

            var expected = new List<int>()
            {
                630,1530,1490
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void group_sum_products2s()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyGroupSum(5, x => x.Price);

            var expected = new List<int>()
            {
                1550,2100
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void get_n_index()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyTake(3);

            var expected = new List<Product>()
            {
                new Product{Id=1, Cost=11, Price=110, Supplier="Odd-e" },
                new Product{Id=2, Cost=21, Price=210, Supplier="Yahoo" },
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void test_any()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyAll(x => x.Price > 200);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void test_All()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MyAny();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void test_Distinct()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MySelect(x => x.Role).MyDistinct();

            var expected = new List<RoleType>()
            {
                RoleType.OP,RoleType.Engineer,RoleType.Manager
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void product_sum()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.MySum(x => x.Price);

            Assert.AreEqual(3650, actual);
        }

        [TestMethod]
        public void any_engineer_above_45_years()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyAny(employee => employee.Role == RoleType.Engineer && employee.Age >= 45);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void is_all_colorball_price_bigger_than180()
        {
            var balls = RepositoryFactory.GetBalls();
            var actual = balls.BenAll(current => current.Prize > 180);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void find_first_age_bigger_than_30()
        {
            var employees = RepositoryFactory.GetEmployees();
            var andy = employees.BenFirst(current => current.Age < 30);
            var expect = new Employee { Name = "Frank", Role = RoleType.Engineer, MonthSalary = 120, Age = 16, WorkingYear = 2.6 };
            expect.ToExpectedObject().ShouldEqual(andy);
        }

        [TestMethod]
        public void find_first_age_bigger_than_30_return_null()
        {
            var employees = RepositoryFactory.GetEmployees();
            var andy = employees.BenFirst(current => current.Age > 60);
            Assert.IsNull(andy);
        }

        [TestMethod]
        public void find_first_age_bigger_than_60_return_eviler()
        {
            var employees = RepositoryFactory.GetEmployees();
            var eviler = employees.BenFirst(current => current.Age > 60, new Employee() { Name = "Eviler" });
            var expect = new Employee() { Name = "Eviler" };
            expect.ToExpectedObject().ShouldEqual(eviler);
        }

        [TestMethod]
        public void one_and_only_one_many_L()
        {
            var balls = RepositoryFactory.GetBalls();
            Assert.ThrowsException<InvalidOperationException>(() => balls.OneAndOnlyOne(current => current.Size == "L"));
        }

        [TestMethod]
        public void one_and_only_one_XL()
        {
            var balls = RepositoryFactory.GetBalls();
            Assert.ThrowsException<InvalidOperationException>(() => balls.OneAndOnlyOne(current => current.Size == "XL"));
        }

        [TestMethod]
        public void one_and_only_one()
        {
            var balls = RepositoryFactory.GetBalls();
            var actual = balls.OneAndOnlyOne(current => current.Size == "M");
            var expect = new ColorBall { Color = Color.Yellow, Size = "M", Prize = 500 };
            expect.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void find_ball_last()
        {
            var balls = RepositoryFactory.GetBalls();
            var actual = balls.MyLast(current => current.Size == "S");
            var expect = new ColorBall { Color = Color.Purple, Size = "S", Prize = 500 };
            expect.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void find_ball_last_null()
        {
            var balls = RepositoryFactory.GetBalls();
            var actual = balls.MyLast(current => current.Size == "XL");
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void is_contain_ball()
        {
            var balls = RepositoryFactory.GetBalls();
            var benCompare = new BenCompare();
            var compare = new ColorBall { Color = Color.Purple, Prize = 300 };
            var matach = YourOwnLinq.BenIsMatch(balls, compare, benCompare);
            Assert.IsFalse(matach);
        }

        [TestMethod]
        public void two_employees_are_equal()
        {
            var first = new List<Employee>
            {
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Andy", Role=RoleType.OP, MonthSalary=80, Age=22, WorkingYear=2.6} ,
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6}
            };

            var second = RepositoryFactory.GetEmployees();

            var isMatch = YourOwnLinq.EmployeeMatch(first, second, new EmployeeCompare());
            Assert.IsFalse(isMatch);
        }

        [TestMethod]
        public void range_1122_to_1124()
        {
            var expect = new List<int> { 1122, 1123, 1124 };
            var actual = GetRange(1122, 3);
            expect.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void repeat()
        {
            var expect = new List<ColorBall>
            {
                new ColorBall{Color = Color.Blue},
                new ColorBall{Color = Color.Blue},
                new ColorBall{Color = Color.Blue},
            };
            var actual = Repeat(new ColorBall { Color = Color.Blue }, 3);
            expect.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        private IEnumerable<ColorBall> Repeat(ColorBall colorBall, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return colorBall;
            }
        }

        private IEnumerable<int> GetRange(int start, int count)
        {
            for (int i = start; i < start + count; i++)
            {
                yield return i;
            }
        }
    }

    internal class EmployeeCompare : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.Age == y.Age && x.Name == y.Name;
        }

        public int GetHashCode(Employee obj)
        {
            throw new NotImplementedException();
        }
    }
}

internal class WithoutLinq
{
    public static List<Product> FindProductByPriceAndSupplier(IEnumerable<Product> products, int lowBoundary, int highBoundary,
        string supplier)
    {
        var result = new List<Product>();
        foreach (var prudcut in products)
        {
            if (prudcut.Price >= lowBoundary && prudcut.Price <= highBoundary && prudcut.Supplier.Equals(supplier))
            {
                result.Add(prudcut);
            }
        }
        return result;
    }
}