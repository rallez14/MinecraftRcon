namespace MinecraftRcon;

internal class Terminal
{
    private static async Task Main(string[] args)
    {
        var (host, port, password) = ParseArguments(args);

        using var client = new Client(host, port);

        try
        {
            if (!await client.AuthenticateAsync(password))
            {
                Console.WriteLine("Authentication failure");
                return;
            }

            const string quitCommand = "exit";
            Console.WriteLine("Remote Console started. Use 'exit' to quit.");

            while (true)
            {
                Console.Write("> ");
                var command = Console.ReadLine();
                if (command == quitCommand) break;

                var resp = await client.SendCommandAsync(command);
                if (resp != null)
                {
                    Console.WriteLine(resp.Body);
                }
                else
                {
                    Console.WriteLine("Error sending command.");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static (string host, int port, string password) ParseArguments(string[] args)
    {
        // Implement argument parsing logic
        // Set default values if arguments are not provided
        return ("127.0.0.1", 25575, "YourRconPasswordHere");
    }
}