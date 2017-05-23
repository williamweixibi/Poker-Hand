using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Poker_Hand_Project
{
    class Program
    {
        const int RANK_NUM = 13, SUIT_NUM = 4, CARD_NUM = 5;
        static bool straight, flush, four, three;
        static int pairs;
        static int[] num_in_rank = new int[RANK_NUM];
        static int[] num_in_suit = new int[SUIT_NUM];
        static public void read_cards()
        {
            /*matrix of bool of whether the card is valid*/
            bool [,] card_exists = new bool [RANK_NUM,SUIT_NUM];
            char ch, rank_ch, suit_ch;
            int rank, suit;
            bool bad_card;
            int cards_read = 0;
            
            for (rank = 0; rank<RANK_NUM; rank++) 
            {
                /*set every element in array to 0*/
              
                num_in_rank[rank] = 0;
                for (suit = 0; suit<SUIT_NUM; suit++)
                {
                    /*fill in false for every card since initially no cards*/
                    card_exists[rank,suit] = false;
                }
            }

            for (suit = 0; suit<SUIT_NUM; suit++)
            {
                /*set every element in array to 0*/
                
                num_in_suit[suit]= 0;
            }

            /*repeated ask until given the correct number of cards*/
            while (cards_read<CARD_NUM) 
            {
                bad_card = false;
                Console.Write("\n");
                Console.Write("Enter a card:");
                
                rank_ch = Console.ReadKey().KeyChar;
                switch (rank_ch) 
                {
                    case '2':           rank = 0; break;
                    case '3':           rank = 1; break;
                    case '4':           rank = 2; break;
                    case '5':           rank = 3; break;
                    case '6':           rank = 4; break;
                    case '7':           rank = 5; break;
                    case '8':           rank = 6; break;
                    case '9':           rank = 7; break;
                    case 't': case 'T': rank = 8; break;
                    case 'j': case 'J': rank = 9; break;
                    case 'q': case 'Q': rank = 10; break;
                    case 'k': case 'K': rank = 11; break;
                    case 'a': case 'A': rank = 12; break;
                    default:            bad_card = true; break;
                }

                suit_ch = Console.ReadKey().KeyChar;
                switch (suit_ch) 
                {
                    case 'c': case 'C': suit = 0;           break;
                    case 'd': case 'D': suit = 1;           break;
                    case 'h': case 'H': suit = 2;           break;
                    case 's': case 'S': suit = 3;           break;
                    default:            bad_card = true;    break;
                }

                if (bad_card)
                {
                        Console.Write("Bad card; ignored.\n");
                }
                else if (card_exists[rank,suit])
                {
                        Console.Write("Duplicate card; ignored.\n");
                }
                else 
                {
                    num_in_rank[rank]++;
                    num_in_suit[suit]++;
                    card_exists[rank,suit] = true;
                    cards_read++;
                }
            }

        }

        static public void analyze_cards()
        {
            int num_consec = 0;
            int rank, suit;

            straight = false;
            flush = false;
            four = false;
            three = false;
            pairs = 0;

            /* check for flush */
            for (suit = 0; suit < SUIT_NUM; suit++)
            {
                if (num_in_suit[suit] == CARD_NUM)
                {
                    flush = true;
                }
            }

            /* check for straight */
            rank = 0;
            while (num_in_rank[rank] == 0)
            {
                rank++;
            }
            for (; rank < RANK_NUM && num_in_rank[rank] > 0; rank++)
            {
                num_consec++;
            }
            if (num_consec == CARD_NUM)
            {
                straight = true;
                return;
            }

            /* check for 4-of-a-kind, 3-of-a-kind, and pairs */
            for (rank = 0; rank < RANK_NUM; rank++)
            {
                if (num_in_rank[rank] == 4) four = true;
                if (num_in_rank[rank] == 3) three = true;
                if (num_in_rank[rank] == 2) pairs++;
            }
        }
        static void print_result()
        {
            if(straight && flush)           Console.Write("Straight flush");
            else if (four)                  Console.Write("Four of a kind");
            else if (three &&pairs == 1)    Console.Write("Full house");
            else if (flush)                 Console.Write("Flush");
            else if (straight)              Console.Write("Straight");
            else if (three)                 Console.Write("Three of a kind");
            else if (pairs == 2)            Console.Write("Two pairs");
            else if (pairs == 1)            Console.Write("Pair");
            else                            Console.Write("High card");
                                            Console.Write("\n\n");
        }



        static void Main(string[] args)
        {
            for (;;)
            {
                read_cards();
                analyze_cards();
                print_result();
            }
        }
    }
}

