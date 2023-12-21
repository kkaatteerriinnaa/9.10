using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp7
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreatePuzzle();
        }

        private void CreatePuzzle()
        {
           
            ImageSource imageSource = new BitmapImage(new Uri("1.jpg", UriKind.Relative));

            ImageSource[] puzzlePieces = new ImageSource[9];
            int pieceWidth = (int)(imageSource.Width / 3);
            int pieceHeight = (int)(imageSource.Height / 3);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    CroppedBitmap croppedBitmap = new CroppedBitmap((BitmapSource)imageSource, new Int32Rect(j * pieceWidth, i * pieceHeight, pieceWidth, pieceHeight));
                    puzzlePieces[i * 3 + j] = croppedBitmap;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                Image puzzlePiece = new Image();
                puzzlePiece.Source = puzzlePieces[i];
                puzzlePiece.AllowDrop = true;
                puzzlePiece.DragEnter += PuzzlePiece_DragEnter;
                puzzlePiece.Drop += PuzzlePiece_Drop;
                puzzleGrid.Children.Add(puzzlePiece);
            }
        }

        private void PuzzlePiece_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        private void PuzzlePiece_Drop(object sender, DragEventArgs e)
        {
            Image puzzlePiece = e.Data.GetData(typeof(Image)) as Image;
            Grid parentGrid = puzzlePiece.Parent as Grid;
            parentGrid.Children.Remove(puzzlePiece);
            Grid dropGrid = sender as Grid;
            dropGrid.Children.Add(puzzlePiece);
        }
    }
}