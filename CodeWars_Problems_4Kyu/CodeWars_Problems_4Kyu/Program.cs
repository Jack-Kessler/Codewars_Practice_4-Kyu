//-----------------------------------------------------------------------------------------------------------------------------
// Sum of Intervals (4-Kyu) (Completed 2/6/2023 - 15th Day of Class)
using System.Collections;

(int, int)[] intervals = {
(-79, -77),
(-444, -430),
(440, 455),
(302, 319),
(141, 155),
(421, 427),
(404, 416),
(-415, -403),
(295, 309),
(-90, -79),
(-248, -244),
(0,0),
(427,440),
(-480, -474)
    };

int i = 0;

//First - the above is called a "Tuple Array"
//Below is how you access each sub element of a tupple array

//for (i = 0; i < myArray.Length; i++)
//{
//    Console.WriteLine(myArray[i].Item1);
//    Console.WriteLine(myArray[i].Item2);
//}

int k = 0;

for (i = 0; i < intervals.Length; i++)
{
    k = 1;
    if (intervals[i].Item1 == intervals[i].Item2)
    {
        k = 1;
    }
    else
    {
        while (i + k < intervals.Length)
        {
            //Case 1: a, c, b, d
            if (intervals[i + k].Item1 >= intervals[i].Item1 && intervals[i + k].Item1 <= intervals[i].Item2 && intervals[i + k].Item2 > intervals[i].Item2)
            {
                intervals[i + k].Item1 = intervals[i].Item2;
            }
            //Case 5: a, c, d, b
            else if (intervals[i + k].Item1 >= intervals[i].Item1 && intervals[i + k].Item1 <= intervals[i].Item2 && intervals[i + k].Item2 <= intervals[i].Item2)
            {
                intervals[i + k].Item1 = 0;
                intervals[i + k].Item2 = 0;
            }
            //Case 2: c, a, d, b
            else if (intervals[i + k].Item1 < intervals[i].Item1 && intervals[i + k].Item2 >= intervals[i].Item1 && intervals[i + k].Item2 <= intervals[i].Item2)
            {
                intervals[i + k].Item2 = intervals[i].Item1;
            }
            //Case 4: c, d, a, b OR a, b, c, d
            else if (intervals[i + k].Item2 <= intervals[i].Item1 || intervals[i + k].Item1 >= intervals[i].Item2)
            {
                //no action needed
            }
            //Case 3: c, a, b, d
            else if (intervals[i + k].Item1 <= intervals[i].Item1 && intervals[i + k].Item2 >= intervals[i].Item2)
            {   
                intervals[i].Item1 = intervals[i+k].Item1;
                intervals[i].Item2 = intervals[i+k].Item2;

                intervals[i+k].Item1 = 0;
                intervals[i+k].Item2= 0;
            }
            else
            {
                Console.WriteLine("Error: Should never happen");
            }
            k++;
        }
    } 
}

//Check to make sure myArray has been modified in the correct way

//for (i = 0; i < myArray.Length; i++)
//{
//    Console.WriteLine(myArray[i].Item1);
//    Console.WriteLine(myArray[i].Item2);
//}

int total = 0;

for (i = 0; i < intervals.Length; i++)
{
    total += intervals[i].Item2 - intervals[i].Item1;
}

Console.WriteLine(total);

//return total;
