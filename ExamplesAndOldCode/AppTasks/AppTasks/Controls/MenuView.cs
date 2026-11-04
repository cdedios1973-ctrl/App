namespace AppTasks.Controls
{
    public class MenuView : ContentView
    {
        public static readonly BindableProperty LIdProperty = BindableProperty.Create(nameof(LId), typeof(string), typeof(MenuView), string.Empty);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MenuView), string.Empty);
        public static readonly BindableProperty HintProperty = BindableProperty.Create(nameof(Hint), typeof(Color), typeof(MenuView), Colors.White);
        public static readonly BindableProperty MenuColorProperty = BindableProperty.Create(nameof(MenuColor), typeof(Color), typeof(MenuView), Colors.White);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MenuView), Colors.DarkGray);
        public static readonly BindableProperty IconImageSourceProperty = BindableProperty.Create(nameof(IconImageSource), typeof(ImageSource), typeof(MenuView), default(ImageSource));
        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(nameof(IconBackgroundColor), typeof(Color), typeof(MenuView), Colors.LightGray);

        public string LId
        {
            get => (string)GetValue(LIdProperty);
            set => SetValue(LIdProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color Hint
        {
            get => (Color)GetValue(HintProperty);
            set => SetValue(HintProperty, value);
        }

        public Color MenuColor
        {
            get => (Color)GetValue(MenuColorProperty);
            set => SetValue(MenuColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public ImageSource IconImageSource
        {
            get => (ImageSource)GetValue(IconImageSourceProperty);
            set => SetValue(IconImageSourceProperty, value);
        }

        public Color IconBackgroundColor
        {
            get => (Color)GetValue(IconBackgroundColorProperty);
            set => SetValue(IconBackgroundColorProperty, value);
        }
    }
}
