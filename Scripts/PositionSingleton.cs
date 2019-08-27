public class PositionSingleton {
    private static PositionSingleton instance;
    public WebbsWalk webbsWalk;
    public ClWalk clWalk;

    public int CurPos = 0;
    public enum Walk
    {
        ComputerLab, 
        WebbsBack,
        WebbsFront
    }
    public Walk walk;

    public static PositionSingleton getInstance() {
        if (instance == null) {
            instance = new PositionSingleton();
        }
        return instance;
    }

    private PositionSingleton(){
        CurPos = 0;
        walk = Walk.WebbsBack;
    }

}
