using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPocketBook.Models;
using MusicPocketBook.ViewModels;

namespace MusicPocketBook.DAL
{
    public class SeedData
    {
        public static List<Keys> GenerateListOfKeys()
        {
            List<Keys> keys = new List<Keys>()
            {
                new Keys()
                {
                    Id = 1,
                    Name = "C-Major",
                    ChordsId = new List<int>()
                    {
                        1, 9, 10, 6, 2, 11, 27
                    },
                    ChordName = new List<string>()
                    {
                        "C", "Dm", "Em", "F", "G", "Am", "Bdim",
                    }
        },

                new Keys()
                {
                    Id = 2,
                    Name = "G-Major",
                    ChordsId = new List<int>()
                    {
                        2, 11, 28, 1, 3, 10, 18
                    },
                    ChordName = new List<string>()
                    {
                        "G", "Am", "Bm", "C", "D", "Em", "F#dim",
                    }
                },

                new Keys()
                {
                    Id = 3,
                    Name = "E-Major",
                    ChordsId = new List<int>()
                    {
                        4, 24, 20, 5, 29, 19, 15
                    },
                    ChordName = new List<string>()
                    {
                        "E", "F#m", "G#m", "A", "B", "C#m", "D#dim",
                    }
                },

                new Keys()
                {
                    Id = 4,
                    Name = "D-Major",
                    ChordsId = new List<int>()
                    {
                        3, 10, 24, 2, 5, 28, 13
                    },
                    ChordName = new List<string>()
                    {
                        "D", "Em", "F#m", "G", "A", "Bm", "C#dim",
                    }
                },

                new Keys()
                {
                    Id = 5,
                    Name = "A-Major",
                    ChordsId = new List<int>()
                    {
                        5, 28, 19, 3, 4, 18, 14
                    },
                    ChordName = new List<string>()
                    {
                        "A", "Bm", "C#m", "D", "E", "F#m", "G#dim",
                    }
                },

                new Keys()
                {
                    Id = 6,
                    Name = "F-Major",
                    ChordsId = new List<int>()
                    {
                        6, 8, 11, 25, 1, 9, 26
                    },
                    ChordName = new List<string>()
                    {
                        "F", "Gm", "Am", "B-flat", "C", "Dm", "Edim",
                    }
                }
            };

            return keys;
        }

        public static List<Chord> GenerateListOfChords()
        {
            List<Chord> chords = new List<Chord>()
            {
                new Chord
                {
                    Id = 1,                    
                    Name = "C",
                    ChordType = Chord.Type.Major,
                    ImageFileName = "",
                    ImageFilePath = @"\DAL\Images\c-major.gif",                    
                    KeysId = new List<int>()
                    {
                        1, 2
                    },
                    KeyNames = new List<string>()
                    {
                        "C-Major", "G-Major"
                    }
                },

                new Chord
                {
                    Id = 2,
                    Name = "G",
                    ChordType = Chord.Type.Major,
                    ImageFileName = "",
                    ImageFilePath = @"\DAL\Images\g-major.gif",
                    KeysId = new List<int>()
                    {
                        1, 2, 4
                    },
                    KeyNames = new List<string>()
                    {
                        "C-Major", "G-Major", "D-Major"
                    }
                },

                new Chord
                {
                    Id = 3,
                    Name = "D",
                    ChordType = Chord.Type.Major,
                    ImageFileName = "",
                    ImageFilePath = @"\DAL\Images\d-major.gif",
                    KeysId = new List<int>()
                    {
                        2, 4, 5
                    },
                    KeyNames = new List<string>()
                    {
                        "G-Major", "D-Major", "A-Major"
                    }
                },

                new Chord
                {
                    Id = 4,
                    Name = "E",
                    ChordType = Chord.Type.Major,
                    ImageFileName = "",
                    ImageFilePath = @"\DAL\Images\e-major.gif",
                    KeysId = new List<int>()
                    {
                        3, 5
                    },
                    KeyNames = new List<string>()
                    {
                        "E-Major", "A-Major"
                    }
                },

                new Chord
                {
                    Id = 5,
                    Name = "A",
                    ChordType = Chord.Type.Major,
                    ImageFileName = "",
                    ImageFilePath = @"\DAL\Images\a-major.gif",
                    KeysId = new List<int>()
                    {
                        3, 4, 5
                    },
                    KeyNames = new List<string>()
                    {
                        "E-Major", "D-Major", "A-Major"
                    }
                },

                new Chord
                {
                    Id = 6,
                    Name = "F",
                    ChordType = Chord.Type.Major,
                    ImageFileName = "",
                    ImageFilePath = @"\DAL\Images\f-major.gif",
                    KeysId = new List<int>()
                    {
                        1, 6
                    },
                    KeyNames = new List<string>()
                    {
                        "C-Major", "F-Major"
                    }
                },

                new Chord
                {
                    Id = 29,
                    Name = "B",
                    ChordType = Chord.Type.Major,
                    KeysId = new List<int>()
                    {
                        3
                    }
                },

                new Chord
                {
                    Id = 7,
                    Name = "Cm",
                    ChordType = Chord.Type.Minor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {

                    }
                },

                new Chord
                {
                    Id = 8,
                    Name = "Gm",
                    ChordType = Chord.Type.Minor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {

                    }
                },

                new Chord
                {
                    Id = 9,
                    Name = "Dm",
                    ChordType = Chord.Type.Minor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        1
                    }
                },

                new Chord
                {
                    Id = 10,
                    Name = "Em",
                    ChordType = Chord.Type.Minor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        1, 2, 4
                    }
                },

                new Chord
                {
                    Id = 11,
                    Name = "Am",
                    ChordType = Chord.Type.Minor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        1, 2
                    }
                },

                new Chord
                {
                    Id = 12,
                    Name = "Fm",
                    ChordType = Chord.Type.Minor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {

                    }
                },

                new Chord
                {
                    Id = 28,
                    Name = "Bm",
                    ChordType = Chord.Type.Minor,
                    KeysId = new List<int>()
                    {
                        2, 4, 5
                    }
                },

                new Chord
                {
                    Id = 13,
                    Name = "C#dim",
                    ChordType = Chord.Type.SharpDiminished,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        5
                    }
                },

                new Chord
                {
                    Id = 14,
                    Name = "G#dim",
                    ChordType = Chord.Type.SharpDiminished,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        5
                    }
                },

                new Chord
                {
                    Id = 15,
                    Name = "D#dim",
                    ChordType = Chord.Type.SharpDiminished,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        3
                    }
                },

                new Chord
                {
                    Id = 16,
                    Name = "E#dim",
                    ChordType = Chord.Type.SharpDiminished,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {

                    }
                },

                new Chord
                {
                    Id = 17,
                    Name = "A#dim",
                    ChordType = Chord.Type.SharpDiminished,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {

                    }
                },

                new Chord
                {
                    Id = 18,
                    Name = "F#dim",
                    ChordType = Chord.Type.SharpDiminished,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        2
                    }
                },

                new Chord
                {
                    Id = 19,
                    Name = "C#m",
                    ChordType = Chord.Type.SharpMinor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        3, 5
                    }
                },

                new Chord
                {
                    Id = 20,
                    Name = "G#m",
                    ChordType = Chord.Type.SharpMinor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        3
                    }
                },

                new Chord
                {
                    Id = 21,
                    Name = "D#m",
                    ChordType = Chord.Type.SharpMinor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {

                    }
                },

                new Chord
                {
                    Id = 22,
                    Name = "E#m",
                    ChordType = Chord.Type.SharpMinor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {

                    }
                },

                new Chord
                {
                    Id = 23,
                    Name = "A#m",
                    ChordType = Chord.Type.SharpMinor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {

                    }
                },

                new Chord
                {
                    Id = 24,
                    Name = "F#m",
                    ChordType = Chord.Type.SharpMinor,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        3, 4, 5
                    }
                },

                new Chord
                {
                    Id = 26,
                    Name = "Edim",
                    ChordType = Chord.Type.Diminished,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        6
                    }
                },

                new Chord
                {
                    Id = 27,
                    Name = "Bdim",
                    ChordType = Chord.Type.Diminished,
                    KeysId = new List<int>()
                    {
                        1
                    }
                },

                new Chord
                {
                    Id = 25,
                    Name = "B-flat",
                    ChordType = Chord.Type.Flat,
                    ImageFileName = "",
                    ImageFilePath = "",
                    KeysId = new List<int>()
                    {
                        6
                    }
                },
            };

            return chords;
        }
    }
}
