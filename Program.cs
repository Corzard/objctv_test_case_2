//����� ����������� ������� � ���� ������������ ������.
//� ���� ���� ���������� count (��� int) � ��� ������, ������� ��������� ��� ���������� ������ � ������: GetCount() �
//AddToCount(int value).
//� ������� �������� ��������� ������������ ��������, ������� � �������� ������, �� ��������� ��������� �������� � count.
//����� ����������� GetCount / AddToCount ���, �����:
//�������� ����� ������ �����������, ��� ������������ � ������� �� ����;
//�������� ������ ������ ��������������� � ������� ������������;
//���� �������� ��������� � �����, �������� ������ ����� ��������� ������


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
