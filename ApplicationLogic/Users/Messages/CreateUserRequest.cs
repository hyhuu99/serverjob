namespace ApplicationLogic.Users.Messages
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsHr { get; set; }
    }
}
