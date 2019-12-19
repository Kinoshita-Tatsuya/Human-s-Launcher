namespace GameLauncher.Infrastructures
{
    public static class GameListDAO
    {                   
        public static string[] Get()
        {
            return FileDataProvider.GetFolderNames("ExternalFiles/");
        }  
    }
}
