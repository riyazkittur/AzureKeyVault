// See https://aka.ms/new-console-template for more information

using KeyVaultStore;

var keyvaultStore = new AzureKeyVaultStore();

try
{
    var isCreated = await keyvaultStore.Store("test-connection-string", "Persist Security Info=True;Server=localhost;Port=5432;Database=galore;User Id=postgres;Password=postgres", default);
    if (isCreated)
    {
        Console.WriteLine("Successfully created");
    }

    var secret = await keyvaultStore.Get("test-connection-string", default);
    Console.WriteLine("Retrieved secret is {0}", secret);
}

catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}




