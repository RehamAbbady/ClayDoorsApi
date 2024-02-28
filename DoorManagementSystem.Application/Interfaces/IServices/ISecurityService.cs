namespace DoorManagementSystem.Application.Interfaces.IServices
{
    public interface ISecurityService
    {
        string HashPin(string pin);
        bool VerifyPin(string providedPin, string storedHash);
    }
}
