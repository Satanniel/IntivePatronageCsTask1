using System;
using System.IO;

namespace IntivePatronageCsTask1{
    class Program{
        
        static void Main(string[] args) {
            //Calling menu
            Menu();

            //Exiting
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void Menu() {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. FizzBuzz");
            Console.WriteLine("2. DeepDive");
            Console.WriteLine("3. DrownItDown");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Choose the option:");
            ConsoleKeyInfo option = Console.ReadKey();
            Console.WriteLine();

            switch (option.Key) {

                case ConsoleKey.D1: {
                    Console.WriteLine("Enter an integer from the 0 - 1000 range.");
                    string fizzInput = Console.ReadLine();
                    //Converting input to int and checking if it's convertable
                    if (Int32.TryParse(fizzInput, out int fizzNumber)) {
                        FizzBuzz(fizzNumber);
                    } else {
                        Console.WriteLine("You didn't enter an integer");
                    }
                    Menu();
                    break;
                }
                case ConsoleKey.D2: {
                    DeepDive();
                    Menu();
                    break;
                }
                case ConsoleKey.D3: {
                    Console.WriteLine("You pressed 3");
                    DrownItDown();
                    Menu();
                    break;
                }
                case ConsoleKey.D4: {
                    break;
                }

                default: {
                    Console.WriteLine("You didn't choose a correct option.");
                    Menu();
                    break;
                }
            }
        }

        static void FizzBuzz(int fizzNumber){
            if (fizzNumber < 0 || fizzNumber > 1000) {
                Console.WriteLine("The entered number is not in the 0 - 1000 range.");
            } else {
                if (fizzNumber % 2 == 0) {
                    Console.Write("Fizz");
                }
                if (fizzNumber % 3 == 0) {
                    Console.Write("Buzz");
                }
                Console.WriteLine();
            }
        }

        static void DeepDive() {
            Console.WriteLine("Enter the desired depth of the directories.");
            string deepInput = Console.ReadLine();
            //Converting input to int and checking if it's convertable
            if (Int32.TryParse(deepInput, out int deepLevel)) {
                string deepPath = String.Empty;
                for (int i = 0; i < deepLevel; i++) {
                    deepPath += "\\" + Guid.NewGuid();
                    //Console.WriteLine(deepPath);
                }
                if(deepLevel > 0) {
                    deepPath = deepPath.Substring(1);
                    Directory.CreateDirectory(deepPath);
                    Console.WriteLine("Created the " + deepPath + " directories.");
                } else {
                    Console.WriteLine("No directories were created.");
                }
            } else {
                Console.WriteLine("You didn't enter an integer");
            }
        }

        static void DrownItDown() {
            int dirNumber = 1;
            string[] baseDirectories = Directory.GetDirectories(".");
            if (baseDirectories.Length == 0) {
                Console.WriteLine("You haven't created any directories. Do you want to use DeepDive to create some? y/n");
                ConsoleKeyInfo option = Console.ReadKey();
                Console.WriteLine();
                int correctButton = 0;
                while (correctButton == 0) {
                    switch (option.Key) {
                        case ConsoleKey.Y: {
                            correctButton++;
                            DeepDive();
                            break;
                        }
                        case ConsoleKey.N: {
                            correctButton++;
                            break;
                        }
                        default: {
                            Console.WriteLine("Press either 'y' for 'yes' or 'n' for 'no'.");
                            option = Console.ReadKey();
                            Console.WriteLine();
                            break;
                        }
                    }
                }
            } else {
                Console.WriteLine("Choose a directory branch to create file in.");
                foreach (string dirName in baseDirectories) {
                    Console.WriteLine(dirNumber + ". " + dirName.Substring(2));
                    dirNumber++;
                }
                string chosenDirString = Console.ReadLine();
                if (Int32.TryParse(chosenDirString, out int chosenDirNumber)) {
                    if (chosenDirNumber < 1 || chosenDirNumber > dirNumber - 1) {
                        Console.WriteLine("The selected branch was incorrect.");
                    } else {
                        Console.WriteLine("How deep should the file be created?");
                        string fileDepthString = Console.ReadLine();
                        if (Int32.TryParse(fileDepthString, out int fileDepthNumber)) {
                            string drownPath = baseDirectories[chosenDirNumber - 1];
                            string[] drownDirectories;
                            bool incorrectDepth = false;
                            //Console.WriteLine(drownPath);
                            for (int i = 1; i < fileDepthNumber; i++) {
                                drownDirectories = Directory.GetDirectories(drownPath);
                                if (drownDirectories.Length == 0) {
                                    Console.WriteLine("The directory structure is not deep enough to create a file at desired depth.");
                                    incorrectDepth = true;
                                    break;
                                }
                                if (drownDirectories.Length > 1) {
                                    Console.WriteLine("There are multiple subdirectiories which shouldn't happen when creating directories using DeepDive. File creation will be cancelled.");
                                    incorrectDepth = true;
                                    break;
                                }
                                drownPath = drownDirectories[0];
                                //Console.WriteLine(i);
                                //Console.WriteLine(drownPath);
                            }
                            if (incorrectDepth == false && fileDepthNumber > 0) {
                                drownPath += "\\drownFile";
                                if (File.Exists(drownPath)) {
                                    Console.WriteLine("The file already exists, do you want to overwrite it? y/n");
                                    ConsoleKeyInfo option = Console.ReadKey();
                                    Console.WriteLine();
                                    int correctButton = 0;
                                    while (correctButton == 0) {
                                        switch (option.Key) {
                                            case ConsoleKey.Y: {
                                                correctButton++;
                                                File.Delete(drownPath);
                                                using (FileStream fs = File.Create(drownPath)) ;
                                                Console.WriteLine("The file was created.");
                                                break;
                                            }
                                            case ConsoleKey.N: {
                                                correctButton++;
                                                break;
                                            }
                                            default: {
                                                Console.WriteLine("Press either 'y' for 'yes' or 'n' for 'no'.");
                                                option = Console.ReadKey();
                                                Console.WriteLine();
                                                break;
                                            }
                                        }
                                    }
                                } else {
                                    using (FileStream fs = File.Create(drownPath)) ;
                                    Console.WriteLine("The file was created.");
                                }
                            }
                        } else {
                            Console.WriteLine("You didn't enter an integer");
                        }
                    }
                } else {
                    Console.WriteLine("You didn't enter an integer");
                }
            }
        }
    }
}
