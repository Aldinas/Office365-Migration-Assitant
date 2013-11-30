using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace _365_Migration_Prep_Tool
{
    class Program
    {
        static void Main(string[] args)
        {


            // Write a brief introduction
            Console.WriteLine("This tool is designed to aid in the deployment of Office 365 for various clients." +
            "\nIt allows you to create a migration .CSV file, as well as provide the \ncorrect details for setting up the users account.\n");

            CaptureClientInfo();
            Console.Clear();

            MainMenu();
        }

        static void CaptureClientInfo()
        {
            // Before we do anything, we want to get all the clients info and store them in globals.
            // Starting with the mail domain.
            Console.WriteLine("Please enter the clients primary mail domain. e.g. GB3.co.uk");
            Globals.ClientDomain = Console.ReadLine();

            // Next we want the users account name:
            Console.WriteLine("Please enter the users Account Name, as displayed in Active Directory:");
            Globals.AccountName = Console.ReadLine();

            //Finally we want the users email address: 
            Console.WriteLine("Please enter the users Email Address, as displayed in Active Directory:");
            Globals.EmailAddress = Console.ReadLine();

            // Display it back to the user to confirm.
            Console.WriteLine("You provided the following details:\n");
            Console.Write("Mail Domain: " + Globals.ClientDomain +
                "\nAccount Name: " + Globals.AccountName +
                "\nEmail Address: " + Globals.EmailAddress + "\n");
            Confirm: Console.WriteLine("Is this correct? [y/n]");
            string correct = Console.ReadLine();

            // If it is correct, continue, if it is not, restart the capture function.
            switch (correct)
            {
                case "y":
                    break;

                case "Y":
                    break;

                case "n":
                    CaptureClientInfo();
                    break;

                case "N":
                    CaptureClientInfo();
                    break;

                default:
                    Console.WriteLine("I'm sorry, I didnt recognise your answer.");
                        goto Confirm;
            }
        }

        static void MainMenu()
        {
            Console.Clear();
            // Write a brief introduction
            Console.WriteLine("This tool is designed to aid in the deployment of Office 365 for various clients." +
            "\nIt allows you to create a migration .CSV file, as well as provide the \ncorrect details for setting up the users account.\n");

            // Initial choice variables.
            string[] options = new string[4] { "1. Create .CSV file", "2. Confirm User's login Credentials", "3. Confirm Mobile Device Configuration", "4. Powershell Tools" };
            string userChoice;

            Console.WriteLine("Please choose from the following " + options.Length + " options:");

            // Print out the options as per the options array.
        ListOptions: for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i]);
            }
            // Prompt the user for a choice and capture the choice. 
            Console.Write("Please type an option and press Enter: ");
            userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    CreateCSV();
                    break;

                case "2":
                    ConfirmCredentials();
                    break;

                case "3":
                    MobileDevHelp();
                    break;

                case "4":
                    PowershellCon();
                    break;

                default:
                    Console.WriteLine("\nSorry that option was not recognised. Please choose from the listed options");
                    goto ListOptions;
            }
        }

        static void CreateCSV()
        {
            Console.Clear();
            // Capture the text to go in the CSV file
            string output = "EmailAddress\n" + Globals.EmailAddress;
            // Find the current users desktop environment
            string desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            // Get the path and the file name.
            string fullFileName = Path.Combine(desktopFolder, Globals.EmailAddress + ".csv");
            // Create the file.
            System.IO.File.WriteAllText(fullFileName, output);

            Console.WriteLine("File has been created and placed on your desktop. Filename: " + Globals.EmailAddress + ".csv");
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadLine();
        }

        static void ConfirmCredentials()
        {
            Console.Clear();
        }

        static void MobileDevHelp()
        {
            // Function to display mobile device config.
        }

        static void PowershellCon()
        {
            // Establish a powershell connection.
        }
    }
}
