namespace Contacts.Services.SignUp
{
    public class CheckType
    {
        public enum CheckEnter
        {
            ChecksArePassed,
            LoginLengthNotValid,
            PasswordLengthNotValid,
            PasswordsNotEqual,
            LoginExist,
            LoginNotDigitalBegin,
            PasswordBigSmallLetterAndDigit

        }
    }
}
