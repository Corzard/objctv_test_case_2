//Нужно реализовать «сервер» в виде статического класса.
//У него есть переменная count (тип int) и два метода, которые позволяют эту переменную читать и писать: GetCount() и
//AddToCount(int value).
//К серверу стучатся множество параллельных клиентов, которые в основном читают, но некоторые добавляют значение к count.
//Нужно реализовать GetCount / AddToCount так, чтобы:
//читатели могли читать параллельно, без выстраивания в очередь по локу;
//писатели писали только последовательно и никогда одновременно;
//пока писатели добавляют и пишут, читатели должны ждать окончания записи


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
var app = builder.Build();

app.MapControllerRoute(name: "api",
    pattern: "/api/{controller}");

app.Run();


static class Server
{
    private static int count = 0;
    private static ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

    public static int GetCount()
    {
        rwLock.EnterReadLock();
        try
        {
            return count;
        }
        finally
        {
            rwLock.ExitReadLock();
        }
    }

    public static void AddToCount(int value)
    {
        rwLock.EnterWriteLock();
        try
        {
            count += value;
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            rwLock.ExitWriteLock();
        }
    }

}
