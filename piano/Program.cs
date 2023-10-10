using NAudio.Wave;
using NAudio.Wave.SampleProviders;

class Piano
{
    private int[] firstOctave = new int[] { 261, 293, 329, 349, 392, 440, 493 };
    private int[] secondOctave = new int[] { 523, 587, 659, 698, 784, 880, 987 };
    private int[] currentOctave;

    private WaveOutEvent waveOut;

    public Piano()
    {
        currentOctave = firstOctave;
        waveOut = new WaveOutEvent();
    }

    public void Play(int noteIndex)
    {
        if (noteIndex >= 0 && noteIndex < currentOctave.Length)
        {
            int frequency = currentOctave[noteIndex];
            PlaySound(frequency);
        }
        else
        {
            Console.WriteLine("Недопустимая нота.");
        }
    }

    public void ChangeOctave(int octaveNumber)
    {
        if (octaveNumber == 1)
        {
            currentOctave = firstOctave;
        }
        else if (octaveNumber == 2)
        {
            currentOctave = secondOctave;
        }
        // Добавьте условия для других октав по аналогии

        Console.WriteLine($"Переключено на октаву {octaveNumber}");
    }

    private void PlaySound(int frequency)
    {
        var sineWaveProvider = new SignalGenerator()
        {
            Type = SignalGeneratorType.Sin,
            Gain = 0.2,
            Frequency = frequency
        };

        waveOut.Init(sineWaveProvider);
        waveOut.Play();
        Thread.Sleep(500); // Воспроизводим ноту в течение 0.5 секунды
        waveOut.Stop();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Piano piano = new Piano();

        Console.WriteLine("Добро пожаловать в пианино!");
        Console.WriteLine("Используйте клавиши от 'A' до ';' для игры нот.");
        Console.WriteLine("Используйте клавиши F1, F2 и т.п. для переключения октавы.");
        Console.WriteLine("Для выхода нажмите 'Q'.");

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Q)
            {
                break;
            }
            else if (keyInfo.Key == ConsoleKey.F1)
            {
                piano.ChangeOctave(1);
            }
            else if (keyInfo.Key == ConsoleKey.F2)
            {
                piano.ChangeOctave(2);
            }
           

           else if (keyInfo.Key >= ConsoleKey.A && keyInfo.Key <= ConsoleKey.L)
            {
                int noteIndex = keyInfo.Key - ConsoleKey.A;
                piano.Play(noteIndex);
            }
        }
    }
}