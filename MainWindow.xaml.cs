using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio.Wave;

namespace VoiceMod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WaveIn recorder;
        private BufferedWaveProvider bufferedWaveProvider;
        private WaveOut player;
        private WaveFormat waveFormat;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnStartRecordingClick(object sender, RoutedEventArgs e)
        {
            // set up the recorder
            recorder = new WaveIn();
            recorder.DataAvailable += RecorderOnDataAvailable;
            waveFormat = new WaveFormat(8000, 8, 1);

            // set up our signal chain
            bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);

            // set up playback
            player = new WaveOut();

            // begin playback & record
            player.Play();
            recorder.StartRecording();
        }

        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            bufferedWaveProvider.AddSamples(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
        }

        private void OnStopRecordingClick(object sender, RoutedEventArgs e)
        {
            // stop recording
            recorder.StopRecording();
            // stop playback
            player.Stop();
        }
    }
}
