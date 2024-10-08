namespace GameUISystem
{
    public enum CanvasType
    {
        none = 0,
        Main,
        UI,
        Popup,
        Tutorial,
        Sysetm,
        Max
    }

    // enum에 따른 값 변환
    public static class EnumExtensions
    {
        public static int ToOrderBaseValue(this CanvasType value)
        {
            return value switch
            {
                CanvasType.Main => 1000,
                CanvasType.UI => 2000,
                CanvasType.Popup=> 3000,
                CanvasType.Tutorial => 4000,
                CanvasType.Sysetm => 5000,
                CanvasType.Max => 0x7FFFF,
                _=>0
            };
        }
    }    
}