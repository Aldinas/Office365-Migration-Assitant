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
            string[] options = new string[6] { "1. Create .CSV file", "2. Confirm User's login Credentials", "3. Confirm Mobile Device Configuration", "4. Powershell Tools", "5. Change user details" ,"6. Quit" };
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

                case "5":
                    ChangeUserDetails();
                    break;


                case "6":
                    Environment.Exit(0);
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
            MainMenu();
        }

        static void ConfirmCredentials()
        {
            Console.Clear();

            // Print out the information stored in globals to identify the account
            Console.Write(
                "Autodiscover server: autodiscover." + Globals.ClientDomain +
                "\nUsername: " + Globals.AccountName + "@" + Globals.ClientDomain +
                "\nEmail Address: " + Globals.EmailAddress +
                "\n\nEnsure that the Username and email address are entered" +
                "\ncorrectly as they are not always identical."
                );
            Console.Write(
                "\nIf you need to check the login details, you can do so" +
                "\nby visiting \nhttp://login.microsoftonline.com and logging in."
                );
            Console.Write("Press any key to return to the main menu.");
            Console.ReadLine();
            MainMenu();
        }

        static void MobileDevHelp()
        {
            Console.Clear();
            // Display information regarding all mobile devices.
            Console.WriteLine("Please choose your mobile device type: " +
                "\n1. iPhone\n2. Android\n3. Windows Phone\n4. BlackBerry");
            Choice: string choice = Console.ReadLine();

            // Switch for various devices. 
            switch (choice)
            {
                // iPhone guide
                case "1":
                    Console.Write(
                        "On iOS, navigate to Settings > Mail, Contacts and Calendars" +
                        "\n> Add Account > Exchange. Enter the details below:" +
                        "\n\nEmail: " + Globals.AccountName + "@" + Globals.ClientDomain +
                        "\nPassword: the users AD password (if synched) or their 365 password." +
                        "\nDescription: " + Globals.ClientDomain + " 365" +
                        "\n\nTap next and the device will autoconfigure, you user can then" +
                        "\nselect what they wish to sync when prompted."
                        );
                    break;
                
                //Android guide
                case "2":
                    Console.WriteLine(
                        "The exact location of mail settings can vary slightly per device" +
                        "\nbut it is usually under Menu > Settings > Mail or Accounts." +
                        "\nOnce there, enter the details as below:" +
                        "\nEmail Address: " + Globals.AccountName + "@" + Globals.ClientDomain +
                        "\nPassword: the users AD password (if synched) or their 365 password." +
                        "\n\nTap Next, the system should auto-populate most of the details" +
                        "\nIf you are prompted t oaccept the security certificate for" +
                        "\nOffice365, tap OK. The device should complete setup automatically."
                        );
                    break;

                // Windows phone
                case "3":
                    Console.WriteLine("To be done when I can get hold of a windows phone");
                    break;

                // Blackberry
                case "4":
                    Console.Write(
                        "BlackBerry's should be to configured to use BlackBerry Cloud Service" +
                        "\nwhere possible, as it provides additional functionality for the" +
                        "\nBlackBerry device.\n\nBefore configuring a BlackBerry, it is recommended" + 
                        "\nto back up the device, as it will" +
                        "\nneed to be reset to factory settings before it can be set up with" +
                        "\nBlackBerry Cloud Service." +
                        "\n\n A more in-depth user guide on how to do this can be found in" +
                        "\nthe Knowledgebase folder in the Documents drive."
                        );
                    break;

                default:
                    Console.WriteLine("Sorry I do not understand the choice. Please enter a number" +
                        "\ncorresponding with your mobile device type.");
                    goto Choice;
            }

            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadLine();
            MainMenu();
            
        }

        static void PowershellCon()
        {
            // Establish a powershell connection.
            Console.Write("Powershell functionality is not available yet." +
                "\nPlease press any key to retun to the main menu");
            Console.ReadLine();
            MainMenu();
        }

        static void ChangeUserDetails()
        {
            Console.Write(
                "What would you like to change?\n1. Mail Domain\n2. User Account and Email Address\n"
                );
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Change mail domain
                    Console.WriteLine("Current mail domain is: " + Globals.ClientDomain);
                    Console.WriteLine("Please enter the new domain and press Enter:");
                    Globals.ClientDomain = Console.ReadLine();
                    Console.WriteLine("The new mail domain is now set to " + Globals.ClientDomain);
                    Console.WriteLine("Press any key to return to the main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "2":
                    // Change email and user account
                    Console.Write("Current User Account is: " + Globals.AccountName +
                        "\nand the current email address is: " + Globals.EmailAddress +
                        "\nPlease enter the new User Account name and press enter: ");
                    Globals.AccountName = Console.ReadLine();
                    Console.WriteLine("Please enter the users email address and press enter: ");
                    Globals.EmailAddress = Console.ReadLine();
                    Console.Write("The new details are as follows: " +
                        "\nUser Name: " + Globals.AccountName +
                        "\nEmail Address: " + Globals.EmailAddress +
                        "\n Press any key to return to the main menu."
                        );
                    Console.ReadLine();
                    MainMenu();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("I do not recognise that option. Please enter one of the following:");
                    ChangeUserDetails();
                    break;
                     
            }
        }
    }
}
