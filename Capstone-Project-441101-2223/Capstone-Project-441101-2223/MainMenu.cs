﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Project_441101_2223
{
    public abstract class MainMenuItem
    {
        public abstract void Select();

    }

    public class ProjectManagerMenu : MainMenuItem
    {
        private ProjectManager _manager;
        private List<MainMenuItem> _menus;
        bool _showMenu;
        public override void Select()
        {
            _showMenu = true;
            do
            {
                Console.Clear();
                _menus.Clear();
                Console.WriteLine("Welocome To Beige\r\n");
                _menus.Add(new AddNewProject(_manager));

                if (_manager._projects.Count > 0)
                {
                    _menus.Add(new EditExsistingProject(_manager));
                    _menus.Add(new RemoveExsisitingProject(_manager));
                    _menus.Add(new ViewAllProjects(_manager));
                    _menus.Add(new ViewPortFolioSummary(_manager));

                }

                Console.WriteLine(MenuExtras.PrintMenuItem(_menus));

                int userInput = MenuExtras.GetItemInRange(1, _menus.Count);

                switch (userInput)
                {
                    case 1:
                        _menus[0].Select(); break;
                    case 2:
                        _menus[1].Select(); break;
                    case 3:
                        _menus[2].Select(); break;
                    case 4:
                        _menus[3].Select(); break;
                    case 5:
                        _menus[4].Select(); break;
                }


            }
            while (_showMenu);

        }

        public ProjectManagerMenu(ProjectManager manager)
        {
            _manager = manager;
            _menus = new List<MainMenuItem>();
        }
    }

    public class AddNewProject : MainMenuItem
    {
        private ProjectManager _manager;
        public override void Select()
        {
            string typeOfProject;
            float cost;

            typeOfProject = MenuExtras.GetTypeOfProject();
            cost = MenuExtras.GetCostOfProject();
            _manager._projects.Add(new Project(cost, typeOfProject));
            MenuExtras.ReturnToMainMenu("Your New Project Has Been Added To Your Porfolio");

        }

        public override string ToString()
        {
            return "Add New Project";
        }

        public AddNewProject(ProjectManager manager)
        {
            _manager = manager;
        }

    }

    public class EditExsistingProject : MainMenuItem
    {
        private ProjectManager _manager;
        private List<MenuItem> _menus;
        public override void Select()
        {
            Console.Clear();
            Console.WriteLine("Which Project Would You Like To Edit?\r\n");


            for (int i = 0; i < _manager._projects.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {_manager._projects[i].ToString()}");
                Console.WriteLine();
            }

            int selectedOption = MenuExtras.GetItemInRange(1, _manager._projects.Count);

            Project selectedProject = _manager._projects[selectedOption - 1];


            _menus.Add(new TypeOfEditToProject(_manager, selectedProject));

            _menus[0].Select();
        }

        public override string ToString()
        {
            return "Edit Existing Project";
        }

        public EditExsistingProject(ProjectManager manager)
        {
            _manager = manager;
            _menus = new List<MenuItem>();
        }
    }

    public class RemoveExsisitingProject : MainMenuItem
    {
        private ProjectManager _manager;
        private List<MenuItem> _menus;
        public override void Select()
        {
            _manager.RemoveProject();
            MenuExtras.ReturnToMainMenu("The Selected Project Was Removed From Your Portfolio");
        }

        public override string ToString()
        {
            return "Remove Existing Project";
        }

        public RemoveExsisitingProject(ProjectManager manager)
        {
            _manager = manager;
            _menus = new List<MenuItem>();
        }


    }

    public class ProjectTableView : MainMenuItem
    {
        private ProjectManager _manager;
        private Project _project;
        private List<float> _purchases;
        private List<float> _sales;

        public override void Select()
        {
            Console.Clear();
            Console.WriteLine($"Project {_project.ID} Overview\r\n");
            Console.SetCursorPosition(Console.WindowLeft, Console.CursorTop + 2);

            for (int i = 0; i < _project.Purchases.Count; i++)
            {
                Console.WriteLine($"{i + 1} {_project.ToString()}");

            }
            Console.SetCursorPosition(Console.WindowLeft, Console.CursorTop + 1);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("List Of All Purchases\r\n");
            foreach (var purchase in _project.Purchases)
            {
                sb.AppendLine(purchase.ToString());
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
            if (_project.Purchases.Count < 1)
            {
                Console.WriteLine("The Current Project Currently Has No Purchases");
            }

            Console.SetCursorPosition(Console.WindowLeft, Console.CursorTop + 1);
            StringBuilder sb2 = new StringBuilder();
            sb2.AppendLine("List Of All Sales\r\n");
            foreach (var sale in _project.Sales)
            {
                sb2.AppendLine(sale.ToString());
            }
            Console.WriteLine(sb2.ToString());

            if (_project.Sales.Count < 1)
            {
                Console.WriteLine("The Current Project Currently Has No Sales");
            }


            MenuExtras.ReturnToMainMenu("");

        }

        public ProjectTableView(ProjectManager manager, Project project)
        {
            _manager = manager;
            _project = project;
            _purchases = new List<float>();
            _sales = new List<float>();
        }
    }

    public class ViewAllProjects : MainMenuItem
    {
        private ProjectManager _manager;
        private List<MenuItem> _menus;
        public override void Select()
        {
            Console.Clear();
            Console.WriteLine("Which Project Would You Like To View?\r\n");
            Console.SetCursorPosition(Console.WindowLeft, Console.CursorTop + 2);


            for (int i = 0; i < _manager._projects.Count; i++)
            {
                Console.WriteLine($"{i + 1} {_manager._projects[i].ToString()}");
                Console.WriteLine("------------------------------------------------------------------------------------------------");

            }

            int selectedOption = MenuExtras.GetItemInRange(1, _manager._projects.Count);

            Project selectedProject = _manager._projects[selectedOption - 1];

            ProjectTableView projectDetails = new ProjectTableView(_manager, selectedProject);

            projectDetails.Select();
        }

        public override string ToString()
        {
            return "View All Projects";
        }

        public ViewAllProjects(ProjectManager manager)
        {
            _manager = manager;
            _menus = new List<MenuItem>();
        }

    }

    public class ViewPortFolioSummary : MainMenuItem
    {
        private ProjectManager _manager;

        public override void Select()
        {
            Console.Clear();
            Console.WriteLine("Entire Portfolio Summary");
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 2);

            float totalOfPurchases = 0;
            float totalOfSales = 0;
            float totalOfRefunds = 0;
            float totalOfProfit = 0;

            for (int i = 0; i < _manager._projects.Count; i++)
            {
                Console.WriteLine($"{i + 1} {_manager._projects[i].ToString()}");
                totalOfPurchases += _manager._projects[i].TotalPurchases;
                totalOfSales += _manager._projects[i].TotalSales;
                totalOfRefunds += _manager._projects[i].Refunds;
                totalOfProfit += _manager._projects[i].Profits;
                Console.WriteLine("------------------------------------------------------------------------------------------------");

            }
            Console.WriteLine("{0,7}{1,9}{2,16}{3,20}{4,21}{5,22}","Total", "NA", Math.Round(totalOfPurchases,2), Math.Round(totalOfSales,2), Math.Round(totalOfRefunds,2), Math.Round(totalOfProfit, 2));
            Console.SetCursorPosition(Console.WindowLeft, Console.CursorTop + 5);
            Console.WriteLine("Press Any Key To Return To Main Menu");

            Console.ReadKey();
        }

        public override string ToString()
        {
            return "View Portfolio Summary";
        }

        public ViewPortFolioSummary(ProjectManager manager)
        {
            _manager = manager;
        }
    }
}
