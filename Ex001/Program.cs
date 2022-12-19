double getNumber(string message)
{
    double result = 0;
    while (true)
    {
        Console.Write(message);
        if (double.TryParse(Console.ReadLine(), out result))
        {
            break;
        }
        else
        {
            Console.WriteLine($"Введено неверное число, повторить ввод");
        }
    }
    return result;
}

Console.WriteLine("Для прямой y = k1 * x + b1");
double k1 = getNumber("Задайте k1: ");
double b1 = getNumber("Задайте b1: ");
Console.WriteLine("Для прямой y = k2 * x + b2");
double k2 = getNumber("Задайте k2: ");
double b2 = getNumber("Задайте b2: ");
//b1 = 2; k1 = 5; b2 = 4; k2 = 9; //Можно раскомментировать для проверки. Ответ: (-0,5, -0,5)
Line line1 = new Line(new Point(0, k1 * 0 + b1), new Point(1, k1 * 1 + b1));
Line line2 = new Line(new Point(0, k2 * 0 + b2), new Point(1, k2 * 1 + b2));

Console.WriteLine(line1);
Console.WriteLine(line2);
Console.WriteLine(line1.Intersect(line2));

Console.ReadLine();

 /// <summary>Класс точки.</summary>
    public class Point : IEquatable<Point>
    {
        /// <summary>Координата X.</summary>
        public double X { get; }
 
        /// <summary>Координата Y.</summary>
        public double Y { get; }
 
        /// <summary>Создаёт точку с заданными координатами.</summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
 
        public override bool Equals(object obj)
            => Equals(obj as Point);
 
        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
 
        public bool Equals(Point point)
        {
            return point != null &&
                   X == point.X &&
                   Y == point.Y;
        }
 
        public override string ToString()
            => $"X={X}; Y={Y}";
    }

     /// <summary>Класс прямой.</summary>
    /// <remarks>В классе определены две точки через которые проходит прямая и её нормализованное уравнение: Ax+By+C=0.</remarks>
    public class Line : IEquatable<Line>
    {
        /// <summary>Первая точка.</summary>
        public Point P1 { get; }
 
        /// <summary>Вторая точка.</summary>
        public Point P2 { get; }
 
        /// <summary>Коэффициент X.</summary>
        public double A { get; }
 
        /// <summary>Коэффициент Y.</summary>
        public double B { get; }
 
        /// <summary>Свободный коэффициент.</summary>
        public double C { get; }
 
        /// <summary>Создаёт линию по коэффициентам нормализованного уравнения.</summary>
        /// <param name="a">Коэффициент X.</param>
        /// <param name="b">Коэффициент Y.</param>
        /// <param name="c">Свободный коэффициент.</param>
        public Line(double a, double b, double c)
        {
            if (a == 0 && b == 0)
                throw new Exception($"Не могут быть одновременно два коэффициента {nameof(a)} и {nameof(b)} равны нулю.");
 
            A = a;
            B = b;
            C = c;
 
            P1 = A != 0 ? new Point(-C / A, 0) : new Point(-C / B, -C / B);
            P2 = B != 0 ? new Point(0, -C / B) : new Point(-C / A, -C / A);
        }
 
        /// <summary>Создаёт линию по точкам.</summary>
        /// <param name="p1">Первая точка.</param>
        /// <param name="p2">Вторая точка.</param>
        public Line(Point p1, Point p2)
        {
            if (p1.Equals(p2))
                throw new Exception("Должны быть несовпадающие точки.");
 
            P1 = p1;
            P2 = p2;
 
            A = P1.Y - P2.Y;
            B = P2.X - P1.X;
            C = P1.X * P2.Y - P2.X * P1.Y;
 
 
        }
 
 
        public override bool Equals(object obj)
            => Equals(obj as Line);
 
        public override int GetHashCode()
        {
 
            double max = A > B ? A : B;
 
            int hashCode = 1369944177;
            hashCode = hashCode * -1521134295 + (A / max).GetHashCode();
            hashCode = hashCode * -1521134295 + (B / max).GetHashCode();
            return hashCode;
        }
 
        public bool Equals(Line line)
        {
            if (line == null)
                return false;
 
            double max = A > B ? A : B;
            double maxL = line.A > line.B ? line.A : line.B;
 
 
            return A / max == line.A / maxL &&
                   B / max == line.B / maxL;
        }
 
        /// <summary>Определяет точку пересечения линий.</summary>
        /// <param name="line1">Первая линия.</param>
        /// <param name="line2">Вторая линия.</param>
        /// <returns>Если есть точка пересечения - возвращается <see cref="Point"/> с координатами.<br/>
        /// Если одна из линий <see langword="null"/> или линии параллельны - возвращается <see langword="null"/>.</returns>
        public static Point Intersect(Line line1, Line line2)
        {
            if (line1 == null || line2 == null || line1.Equals(line2))
                return null;
 
            double denominator = (line1.A * line2.B - line2.A * line1.B);
            if (denominator == 0)
                return null;
 
            double x = (line1.B * line2.C - line2.B * line1.C) / denominator;
            double y = (line2.A * line1.C - line1.A * line2.C) / denominator;
 
            return new Point(x, y);
        }
 
        /// <summary>Определяет точку пересечения с другой линией.</summary>
        /// <param name="line">Другая линия.</param>
        /// <returns>Если есть точка пересечения - возвращается <see cref="Point"/> с координатами.<br/>
        /// Если другая линия <see langword="null"/> или линии параллельны - возвращается <see langword="null"/>.</returns>
        public Point Intersect(Line line)
                 => Intersect(this, line);
 
        public override string ToString()
            => $"{A}*x {(B >= 0 ? "+" : "-")} {Math.Abs(B)}*y {(C >= 0 ? "+" : "-")} {Math.Abs(C)} = 0";
    }