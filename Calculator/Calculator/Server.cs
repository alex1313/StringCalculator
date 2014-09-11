using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace StringCalculator
{
    public class SocketServer
    {
        private const int Port = 4050;

        static void Main(string[] args)
        {
            // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            var ipEndPoint = new IPEndPoint(ipAddr, Port);

            // Создаем сокет Tcp/Ip
            var sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string expression = null;

                    // Мы дождались клиента, пытающегося с нами соединиться

                    var bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    expression += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    // Показываем данные на консоли
                    Console.Write("Полученное выражение: " + expression + "\n\n");

                    var calculator = new Calculator();

                    string[] polishExpression = calculator.PolishNotation(calculator.SplitExpression(expression));
                    string result = polishExpression.Aggregate("", (current, symbol) => current + symbol);
                    result += "\n";

                    result += calculator.Calculate(expression);

                    // Отправляем ответ клиенту
                    byte[] msg = Encoding.UTF8.GetBytes(result);
                    handler.Send(msg);

                    if (expression.IndexOf("<TheEnd>", System.StringComparison.Ordinal) > -1)
                    {
                        Console.WriteLine("Сервер завершил соединение с клиентом.");
                        break;
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }

        }
    }
}