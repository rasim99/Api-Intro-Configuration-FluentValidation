namespace FirstApiProject.Extension
{
    public static class Extension
    {
        public static int DateToAge(this DateTime dateTime)
        {
            return DateTime.Now.Year - dateTime.Year;
        }
    }
}
