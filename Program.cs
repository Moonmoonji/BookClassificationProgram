using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace 도서분류
{
    class Time2Book
    {
        public Dictionary<long, Book> time2book;

        public Time2Book()
        {
            time2book = new Dictionary<long, Book>();
        }
    }

    class Book
    {
        public List<string> Title;
        public List<bool> Used;
        //public List<long> Time;

        public Book()
        {
            Title = new List<string>();
            Used = new List<bool>();
            //Time = new List<long>();
        }
    }

    class Sort_Book
    {
        public List<string> Title;
        public List<bool> Used;
        public List<long> Time;

        public Sort_Book(Book books)
        {
            Title = new List<string>();
            Used = new List<bool>();
            Time = new List<long>();

            Title = books.Title;
            Used = books.Used;
        }

        public void Sort_Books_Info() //메소드 
        {
            this.Time.Reverse();
        }
    }

    class Program
    {
        public Program() { }

        public string Add_Books()
        {
            Console.WriteLine("도서제목을 입력하세요:");
            string temp_book_name = Console.ReadLine();
            return temp_book_name;
        } /* Add_Books() */

        public void Borrow(Time2Book ttb)
        {
            // 1. 빌릴 책을 입력
            Console.WriteLine("빌릴책을 입력하세요: ");
            string aaa = Console.ReadLine();

            //책이 아무것도 등록되어 있지 않을 때
            if (ttb.time2book.Count == 0)
            {
                Console.WriteLine("책이 등록되어 있지 않습니다. 새로운 도서를 추가해 주세요.");
            }
            // 2. 입력된 책 제목을 찾기
            else
            {
                int index = 0;
                foreach (var entry in ttb.time2book)
                {
                    index++;
                    if (string.Compare(entry.Value.Title[0], aaa) == 0)
                    {
                        Console.WriteLine("해당하는 책을 찾았습니다");
                        if (entry.Value.Used[0] == false)
                        {
                            // 빌릴꺼야? 응
                            Console.WriteLine("해당하는 책을 정말 빌리시겠습니까?");
                            Console.WriteLine("1: 네");
                            Console.WriteLine("2: 아니요");
                            int input = int.Parse(Console.ReadLine());

                            if (input == 1)
                            {
                                entry.Value.Used[0] = true; 
                                Console.WriteLine("대출 완료되었습니다.");
                                break;
                            }

                            else
                            {
                                Console.WriteLine("대출이 취소되었습니다.");
                                break;
                            }

                        }
                        else
                        {
                            // 책은 있지만 못빌려
                            Console.WriteLine("현재 해당하는 책을 빌릴 수 없습니다.");
                            break;
                        }
                    }
                    else
                    {
                        // 책 제목을 못찾았다
                        if (index == ttb.time2book.Count)
                            Console.WriteLine("해당하는 책을 찾지 못했습니다.");
                    }
                }

            }
        }


        public void Return(Time2Book ttb)
        {

            //책이 아무것도 등록되어 있지 않을 때
            if (ttb.time2book.Count == 0)
            {
                Console.WriteLine("책이 등록되어 있지 않습니다. 새로운 도서를 추가해 주세요.");
            }
            //반납할 책 제목을 찾기
            else
            {

                int count = 0;
                foreach (var entry in ttb.time2book)
                {
                    if (entry.Value.Used[0] == false)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }


                if (count == ttb.time2book.Count)
                {
                    Console.WriteLine("현재 대여된 책이 없습니다");
                }
                else
                {
                    int index = 0;
                    //반납할 책을 입력
                    Console.WriteLine("반납할 책을 입력하세요: ");
                    string aaa = Console.ReadLine();

                    foreach (var entry in ttb.time2book)
                    {
                        index++;
                        if (string.Compare(entry.Value.Title[0], aaa) == 0)
                        {
                            // 찾았다 ~~ 알려주고
                            Console.WriteLine("해당하는 책을 찾았습니다");
                            if (entry.Value.Used[0] == true)
                            {
                                // 빌릴꺼야? 응
                                Console.WriteLine("해당하는 책을 정말 반납 하시겠습니까?");
                                Console.WriteLine("1: 네");
                                Console.WriteLine("2: 아니요");
                                int input = int.Parse(Console.ReadLine());

                                if (input == 1)
                                {
                                    entry.Value.Used[0] = false;
                                    Console.WriteLine("반납 완료되었습니다.");
                                }
                                else
                                {
                                    Console.WriteLine("반납이 취소되었습니다.");
                                }
                                break;
                            }
                            else
                            {
                                // 해당하는 책은 있지만 반납할 수 없는 상태 
                                Console.WriteLine("현재 해당하는 책을 반납할 수 없습니다.");
                                break;
                            }
                        }

                        else
                        {
                            if (index == ttb.time2book.Count)
                                Console.WriteLine("해당하는 책을 찾지 못했습니다.");
                        }
                    }
                }
            }
        }

        public void Checkusable(Time2Book ttb)
        {

            // 메뉴
            Console.WriteLine("원하는 보기 방식을 입력해 주세요");

            // 선택
            Console.WriteLine("1 : 오래된 도서 순으로 보기");
            Console.WriteLine("2 : 최근 추가된 도서 순으로 보기 ");

            int input = int.Parse(Console.ReadLine());


            if (input == 1)
            {
                // 1. 책을 넣은대로 보여주기
                foreach (var entry in ttb.time2book)
                {
                    string temp_title = entry.Value.Title[0];
                    bool temp_used = entry.Value.Used[0];
                    Console.WriteLine("책 이름은 {0}이고 현재는 {1} 상태입니다", temp_title, temp_used);

                }
            }

            else
            {
                // 2. 시간 역순으로 보여주기
                var abc = ttb.time2book.OrderByDescending(loopidx => loopidx.Key).ToDictionary(loopidx => loopidx.Key, loopidx => loopidx.Value);
                foreach (var entry in abc)
                {
                    string temp_title = entry.Value.Title[0];
                    bool temp_used = entry.Value.Used[0];
                    Console.WriteLine("책 이름은 {0}이고 현재는 {1} 상태입니다.", temp_title, temp_used);

                }
            }
        }

        public static void Main(string[] args)
        {
            int input;
            Program p1 = new Program();
            Time2Book t2b = new Time2Book();
            //Book books = new Book();

            while (true)
            {
                Console.WriteLine("실행하고자 하는 기능을 선택하세요");
                Console.WriteLine("1: 새로운 도서 추가 ");
                Console.WriteLine("2: 책 대여 ");
                Console.WriteLine("3: 책 반납 ");
                Console.WriteLine("4: 현재 도서 목록 확인 ");
                Console.WriteLine("Exit: 아무키나 클릭");

                input = int.Parse(Console.ReadLine());

                if ((input >= 1) && (input <= 4))
                {
                    switch (input)
                    {
                        case 1:
                            {
                                // 1번 호출
                                string aaa = p1.Add_Books();
                                Book books = new Book();
                                books.Title.Add(aaa);
                                books.Used.Add(false);
                                string str_cur_time = DateTime.Now.ToString("yyMMddHHmmss");
                                long cur_time = long.Parse(str_cur_time);
                                t2b.time2book.Add(cur_time, books);

                                Console.WriteLine("도서 등록이 완료되었습니다.");
                                Thread.Sleep(1500);
                                Console.Clear();
                                break;
                            }


                        case 2:
                            {
                                // 2번 호출
                                p1.Borrow(t2b);
                                Thread.Sleep(1500);
                                Console.Clear();
                                break;

                            }


                        case 3:
                            {
                                // 3번 호출
                                p1.Return(t2b);
                                Thread.Sleep(1500);
                                Console.Clear();
                                break;
                            }


                        case 4:
                            {
                                // 4번 호출
                                p1.Checkusable(t2b);
                                Console.WriteLine("계속 하시려면 아무 키나 누르세요.");
                                ConsoleKeyInfo cki = Console.ReadKey(true);
                                Console.WriteLine("{0}", cki.Key);
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;
                            }

                    }

                }
                else
                {
                    break;
                    // 종료
                }
            }
        }

    }
}