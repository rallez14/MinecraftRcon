# Minecraft Rcon

## Description
A simple program for remote management of a Minecraft server using the RCON protocol.

## Features
- Authentication using RCON password.
- Sending commands to the server using the RCON protocol.

## Requirements
- .NET 5.0 or later

## Configuration
Default settings, such as host address, port, and RCON password, can be changed in the `Terminal.cs` file under the `Default Configuration` section.

## Running the Program
To run the program as a standalone application, follow these steps:

1. Open a terminal in the project directory.
2. Run the application using the following command:
    ```bash
    dotnet run
    ```

3. Upon launch, enter the required information such as host address, port, and RCON password.

## Using as a NuGet Package
If you want to use this project as a NuGet package in your own project, follow these steps:

1. Install the NuGet package using the package manager console:
    ```bash
    Install-Package MinecraftRcon
    ```

2. In your code, you can use the `Client` class to interact with the Minecraft server using RCON:
    ```csharp
    using MinecraftRcon;

    class Program
    {
        static async Task Main(string[] args)
        {
            var host = "your_server_host";
            var port = 25575;
            var password = "your_rcon_password";

            using var client = new Client(host, port);

            if (await client.AuthenticateAsync(password))
            {
                var command = "your_minecraft_command";
                var response = await client.SendCommandAsync(command);

                Console.WriteLine(response?.Body ?? "Command failed");
            }
            else
            {
                Console.WriteLine("Authentication failure");
            }
        }
    }
    ```

## Author
rallez

## Contact
Discord: rallez
Email: rallezg1@gmail.com

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
