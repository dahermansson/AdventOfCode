using System;
using System.Linq;
using System.Collections.Generic;
namespace dag12
{
    class Program
    {
        static void Main(string[] args)
        {
            Star1();
            Star2();
        }

        static void Star2()
        {
            HashSet<Moon> setMoons = new HashSet<Moon>();
            var moons = new List<Moon>(){
                new Moon(-1, 0, 2), 
                new Moon(2, -10, -7),
                new Moon(4, -8, 8),
                new Moon(3, 5, -1)
            };

            moons = new List<Moon>(){
                new Moon(-8, -10, 0), 
                new Moon(5, 5, 10),
                new Moon(2, -7, 3),
                new Moon(9, -8, -3)
            };

            var startMoons = moons.Select(t => t.Clone()).ToList();
            var simulations = 0;
            var run = true;
            while (run)
            {
                Simulate(moons);
                simulations++;
                if(moons.TrueForAll(t => t.ZeroVelocity()))
                {
                    Console.WriteLine(simulations);
                    run = false;
                    for (int i = 0; i < 4; i++)
                    {
                        if(!moons[i].PositionEquals(startMoons[i]))
                        {
                            run = true;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine($"Star 2: {simulations}");
        }

        static void Star1()
        {
            var moons = new Moon[4]{
                new Moon(-9, -1, -1), 
                new Moon(2, 9, 5),
                new Moon(10, 18, -12),
                new Moon(-6, 15, -7)
            };
            for (int i = 0; i < 1000; i++)
                Simulate(moons);
            Console.WriteLine($"Star 1: {moons.Sum(t => t.TotalEnergy())}");
        }

        public static void Simulate(IEnumerable<Moon> moons)
        {
            foreach (var moon in moons)
                foreach (var m in moons)
                {
                    if(moon == m)
                        continue;
                    moon.AddGravity(m);

                }
            foreach (var moon in moons)
                moon.ApplyVelocity();
        }
    }

    public class Moon
    {
        public Moon(int x, int y, int z)
        {
            PX = x;
            PY = y;
            PZ = z;
        }
        public Moon Clone()
        {
            return new Moon(this.PX, this.PY, this.PZ){ VX = this.VX, VY = this.VY, VZ = this.VZ};
        }

        public bool ZeroVelocity()
        {
            return VX == 0 && VY == 0 && VZ == 0;
        }

        public bool PositionEquals(Moon b)
        {
            return this.PX == b.PX && this.PY == b.PY && this.PZ == b.PZ;
        }
        public int TotalEnergy()
        {
            return (Math.Abs(PX) + Math.Abs(PY) + Math.Abs(PZ)) * (Math.Abs(VX) + Math.Abs(VY) + Math.Abs(VZ));
        }

        public override string ToString()
        {
            return $"pos=<x={PX}, y={PY}, z={PZ}>, vel=<x={VX}, y={VY}, z={VZ}>";
        }

        public int PX { get; set; }
        public int PY { get; set; }
        public int PZ { get; set; }
        public int VX { get; set; }
        public int VY { get; set; }
        public int VZ { get; set; }

        public void AddGravity(Moon moon)
        {
            VX += GetChange(this.PX, moon.PX);
            VY += GetChange(this.PY, moon.PY);
            VZ += GetChange(this.PZ, moon.PZ);
        }

        public void ApplyVelocity()
        {
            PX += VX;
            PY += VY;
            PZ += VZ;
        }

        private int GetChange (int a, int b)
        {
            if (a < b)
                return 1;
            if (a > b)
                return -1;
            return 0; 
        }

    }
}
