//Next smaller number with the same digits
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

long n = 40112333522;
long nCopy = n;
long result = 0;

int duplicateCounter = 0;
int zeroCounter = 0;
int notZeroIndex = 0;
int numberOfDuplicates = 0;
int i = 0;
int j = 0;
int x = 0;
int a = -1;

int counter = 1;
int counter2 = 0;

bool done = false;

var resultList = new List<long>();
var tempList = new List<long>();


string str = n.ToString();

List<long> originalOrder = new List<long>();

while (nCopy > 0)
{
    originalOrder.Add(nCopy % 10);
    nCopy = nCopy / 10;
}
originalOrder.Reverse();


int originalFirstDigit = (int)originalOrder[0];

for (i = 0; i < originalOrder.Count(); i++)
{
    if (originalOrder[i] == originalFirstDigit) //Counting how many duplicates of the original first digit there are
    {
        duplicateCounter++;
    }
    else if (originalOrder[i] == 0) //Counting how many zeros digits there are
    {
        zeroCounter++;
    }
    if (duplicateCounter == originalOrder.Count()) //Case: all digits are the same (i.e. 555 or 8888)
    {
        result = -1;
    }
}

numberOfDuplicates = duplicateCounter - 1;

if (originalOrder.Count() == 1) //Case 1: Only 1 digit to begin with (i.e. 1 - 9)
{
    result = -1;
}
else if (originalOrder.Count() == 2) //Case 2: Only 2 digits to begin with (i.e. 10 - 99)
{
    if (originalOrder[1] == 0)
    {
        result = -1;
    }
    else if (originalOrder[1] >= originalOrder[0])
    {
        result = -1;
    }
    else
    {
        resultList.Add(originalOrder[1]);
        resultList.Add(originalOrder[0]);
        foreach (long p in resultList)
        {
            result = 10 * result + p;
        }
    }
}
else if (originalOrder.Count() == 3) //Case 3: Three or more digits originally (i.e. 100 ~ )
{
    if (zeroCounter == 2) //Case: all but original first digit are zeros.
    {                                       //Remember - Assuming original first digit not equal to zero.
        result = -1;
    }
    else if (zeroCounter == 1) //Case: one zero and two non-zero digits
    {
        if (numberOfDuplicates == 0) //Case: two non-zero digits are NOT equal
        {
            if (originalOrder[1] == 0) //Ex. 605 or 607
            {
                if (originalOrder[0] < originalOrder[2]) //Ex. 607
                {
                    result = -1;
                }
                else // Ex. 605
                {
                    resultList.Add(originalOrder[2]);
                    resultList.Add(originalOrder[0]);
                    resultList.Add(originalOrder[1]);
                    foreach (long p in resultList)
                    {
                        result = 10 * result + p;
                    }
                }
            }
            else if (originalOrder[2] == 0) //Ex. 670 ---> 607 OR 650 ---> 605
            {
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[1]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else
            {
                Console.WriteLine("Error #1: This should never happen");
            }
        }
        else //Case: one zero and two non-zero digits; two non-zero digits are equal (ex. 550 & 505)
        {
            if (originalOrder[0] == originalOrder[1]) //ex. 550
            {
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[1]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else //ex. 505
            {
                result = -1;
            }
        }
    }
    else if (zeroCounter == 0) //Case: No zeros (ex.521, 512, 516, 567, 544, 566 Duplicates w/ first digit: 559, 551, 595, 545)
    {
        if (numberOfDuplicates == 1) //First, tackle cases with duplicate of first digit. Ex. 559, 551, 595, 545
        {
            if (originalOrder[0] == originalOrder[1]) //Ex. 559, 551
            {
                if (originalOrder[0] > originalOrder[2]) //Ex. 551 ---> 515
                {
                    resultList.Add(originalOrder[0]);
                    resultList.Add(originalOrder[2]);
                    resultList.Add(originalOrder[1]);
                    foreach (long p in resultList)
                    {
                        result = 10 * result + p;
                    }
                }
                else // Ex. 559
                {
                    result = -1;
                }
            }
            else if (originalOrder[0] == originalOrder[2]) //Ex.  595, 515
            {
                if (originalOrder[0] < originalOrder[1]) //Ex. 595 ---> 559
                {
                    resultList.Add(originalOrder[0]);
                    resultList.Add(originalOrder[2]);
                    resultList.Add(originalOrder[1]);
                    foreach (long p in resultList)
                    {
                        result = 10 * result + p;
                    }
                }
                else //Ex. 515 ---> 155
                {
                    resultList.Add(originalOrder[1]);
                    resultList.Add(originalOrder[0]);
                    resultList.Add(originalOrder[2]);
                    foreach (long p in resultList)
                    {
                        result = 10 * result + p;
                    }
                }
            }
        }
        else if (originalOrder[0] != originalOrder[1] && originalOrder[1] == originalOrder[2])
        //No zeros. No first digit duplicates. Other two digits are duplicates. Ex. 544, 566, 454, 656
        {

            if (originalOrder[0] > originalOrder[1]) //Ex. 544 ---> 454
            {
                resultList.Add(originalOrder[1]);
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else // Ex. 566
            {
                result = -1;
            }
        }
        else if (originalOrder[0] != originalOrder[1] && originalOrder[0] == originalOrder[2])
        //No zeros. No first digit duplicates. Other two digits are duplicates
        {
            if (originalOrder[0] < originalOrder[1]) //Ex. 454 ---> 445
            {
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[1]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else // Ex. 656 ---> 566
            {
                resultList.Add(originalOrder[1]);
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
        }
        else if (numberOfDuplicates == 0 && originalOrder[0] != originalOrder[1] &&
                    originalOrder[0] != originalOrder[2] && originalOrder[1] != originalOrder[2])
        //No Zeros. No duplicates at all. All three digits are different. Ex. 531, 513, 135
        {
            if (originalOrder[2] < originalOrder[1]) //Ex. 531  --> 513
            {
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[1]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else if (originalOrder[0] > originalOrder[1] && originalOrder[2] > originalOrder[1] && originalOrder[0] > originalOrder[2]) //Ex. 513 ---> 351
                                                                                                                                        //(Note: 531 case was taken care of above)
            {
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[0]);
                resultList.Add(originalOrder[1]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else if (originalOrder[0] > originalOrder[1] && originalOrder[2] > originalOrder[1] && originalOrder[2] > originalOrder[0]) //Ex. 315 ---> 351
                                                                                                                                        //(Note: 531 case was taken care of above)
            {
                resultList.Add(originalOrder[1]);
                resultList.Add(originalOrder[2]);
                resultList.Add(originalOrder[0]);
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else if (originalOrder[2] > originalOrder[1]) //Ex. 135
            {
                result = -1;
            }
            else
            {
                Console.WriteLine("Error #2: Should never happen.");
            }
        }
        else
        {
            Console.WriteLine("Error #3: Should never happen.");
        }
    }
    else
    {
        Console.WriteLine("Error #4: Should never happen");
    }
}
else //*** 4 DIGITS OR MORE ***
{
    if (zeroCounter == originalOrder.Count() - 1) // Case 1: All but 1 digit (original first digit) are zeros. Ex. 500000 or 70000000000000
    {
        result = -1;
    }
    else if (originalOrder[originalOrder.Count() - 1] < originalOrder[originalOrder.Count() - 2]) //Case 2: Can just flip last two digits
    {
        for (i = 0; i < originalOrder.Count() - 2; i++)
        {
            resultList.Add(originalOrder[i]); //Create duplicate list of original order array
        }
        for (i = 0; i < 1; i++)
        {
            resultList.Add(originalOrder[originalOrder.Count() - 1]);
        }
        for (i = 0; i < 1; i++)
        {
            resultList.Add(originalOrder[originalOrder.Count() - 2]);
        }
        foreach (long p in resultList)
        {
            result = 10 * result + p;
        }
    }
    else if (zeroCounter == originalOrder.Count() - 2) // Case 3: All but 2 digits (original first digit + one more) are zeros.
    {
        if (originalOrder[originalOrder.Count() - 1] != 0) //Ex. 50005, 500006, 500004
        {
            if (originalOrder[originalOrder.Count() - 1] < originalOrder[0]) //Ex. 500004 & 500001
            {
                for (i = 0; i < originalOrder.Count(); i++)
                {
                    if (i == 0)
                    {
                        resultList.Add(originalOrder[originalOrder.Count() - 1]);
                    }
                    else if (i == 1)
                    {
                        resultList.Add(originalOrder[0]);
                    }
                    else
                    {
                        resultList.Add(0);
                    }
                }
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
            else //Ex. 500005 & 500007
            {
                result = -1;
            }
        }
        else
        {
            for (i = 1; i < originalOrder.Count(); i++) //Assuming original first digit is non-zero. Ex. 500500 or 507000
            {
                if ((originalOrder[i] != 0))
                {
                    notZeroIndex = i;       //i is index of non-zero digit that is not the original first digit
                    break;
                }
            }
            resultList.Add(originalOrder[0]); //have to first add first digit into resultList.
            for (i = 1; i < originalOrder.Count(); i++)
            {
                if (i == notZeroIndex)
                {
                    resultList.Add(originalOrder[i + 1]);
                }
                else if (i == notZeroIndex + 1)
                {
                    resultList.Add(originalOrder[i - 1]);
                }
                else
                {
                    resultList.Add(originalOrder[i]);
                }
            }
            foreach (long p in resultList)
            {
                result = 10 * result + p;
            }
        }
    }
    else
    {
        if (originalOrder[1] == 0) //Zero is in index 1 spot.
        {
            tempList.Clear();
            for (i = originalOrder.Count() - 1; i > 1; i--) //starting from end of list through index 2
            {
                if (originalOrder[i] >= originalOrder[i-1])
                {
                    counter2++;
                }
                if (counter2 == originalOrder.Count() - 2) //Every number to right of zero in index 1 spot is greater than or equal to first digit.
                {
                    done = true;
                    break;
                }
            }

        }
        if (done == true)
        {
            result = -1;
        }
        else
        {
            // Gerenic method of starting from the right, checking back
            tempList.Clear();
            for (i = originalOrder.Count() - 2; i >= 1; i--)
            {
                if (originalOrder[i] < originalOrder[i - 1])
                {
                    for (j = i + 1; j < originalOrder.Count(); j++)
                    {
                        tempList.Add(originalOrder[j]); //adds all digits to right of swap point.
                                                        //Note: we already know last digit is greater than second to last digit from logic above.
                    }
                    tempList.Sort();
                    for (j = 0; j < tempList.Count(); j++)
                    {
                        if (tempList[j] < originalOrder[i - 1] && tempList[j] > a) //Note: tempList cannot have 0 as an element
                        {
                            a = (int)tempList[j];
                        }
                    }
                    if (a != -1)
                    {
                        for (j = i + 1; j < originalOrder.Count(); j++)
                        {
                            if (originalOrder[j] == a) //Note: tempList cannot have 0 as an element
                            {
                                break;      //Finding index (j) in originalOrder where value = a.
                            }
                        }
                        tempList.Clear();
                        for (x = originalOrder.Count() - 1; x > j; x--)
                        {
                            tempList.Add(originalOrder[x]); //Add everything to right of j
                        }
                        for (x = i - 1; x < j; x++)
                        {
                            tempList.Add(originalOrder[x]); //add everything to the left of j including digit at i-1 position.
                        }
                        tempList.Sort();
                        tempList.Reverse();
                        resultList.Clear();
                        if (i - 1 == 0)
                        {
                            resultList.Add(originalOrder[j]);
                            for (x = 0; x < tempList.Count(); x++) //Last, add digits in temp list from greatest to least
                            {
                                resultList.Add(tempList[x]);
                            }
                            foreach (long p in resultList)
                            {
                                result = 10 * result + p;
                            }
                        }
                        else
                        {
                            for (x = 0; x < i - 1; x++)
                            {
                                resultList.Add(originalOrder[x]); //Start building result list. First add everything to left of i - 1.
                            }
                            resultList.Add(originalOrder[j]); //Next, swap out next largest digit after i -1 in i - 1 position.
                            for (x = 0; x < tempList.Count(); x++) //Last, add digits in temp list from greatest to least
                            {
                                resultList.Add(tempList[x]);
                            }
                            foreach (long p in resultList)
                            {
                                result = 10 * result + p;
                            }
                            break;
                        }
                    }
                    else // case where no other digit to the right of i is less than digit to the left of i.
                         // In this case, need to slide i one to the left (in i - 1 position) and reorder everything to the right, greatest to least
                    {
                        tempList.Add(originalOrder[i - 1]);
                        tempList.Sort();
                        tempList.Reverse();
                        for (j = 0; j < i - 1; j++)
                        {
                            resultList.Add(originalOrder[j]); //Adds up to swap point
                        }
                        resultList.Add(originalOrder[i]); //Next add i
                        for (j = 0; j < tempList.Count(); j++) //Lastly, add templist
                        {
                            resultList.Add(tempList[j]);
                        }
                        foreach (long p in resultList)
                        {
                            result = 10 * result + p;
                        }
                        break;
                    }

                }
                else
                {
                    counter++;
                    if (counter > originalOrder.Count() - 2)
                    {
                        result = -1;
                        break;
                    }
                }
            }
        }
    }
}
Console.WriteLine(result);


//-----------------------------------------------------------------------------------------------------------------------------
// Sum of Intervals (4-Kyu) (Completed 2/6/2023 - 15th Day of Class)

//**** Efficient Method ****

//(int, int)[] intervals = {

//   (-7, 8), (-2, 10), (5, 15), (2000, 3150), (-5400, -5338)

////(-35, -25),
////(178, 179),
////(422, 436),
////(-358, -349),
////(403, 416),
////(125, 131),
////(462, 479),
////(305, 319),
////(-338, -328),
////(248, 257),
////(-378, -359),
////(-234, -214),
////(164, 170),
////(-368, -348)

////expected 150

////was 159
//    };

//int i = 0;

//(int, int)[] intervals2 = new (int, int)[intervals.Length];

//for (i = 0; i < intervals.Length; i++)
//{
//    intervals2[i].Item1 = intervals[i].Item1;
//    intervals2[i].Item2 = intervals[i].Item2;
//    //Console.WriteLine(intervals2[i].Item1);
//    //Console.WriteLine(intervals2[i].Item2);
//}

//intervals2 = intervals2.OrderBy(t => t.Item1).ThenBy(t => t.Item2).ToArray();

////for (i = 0; i < intervals2.Length; i++)
////{
////    //    Console.WriteLine(intervals2[i].Item1);
////    //    Console.WriteLine(intervals2[i].Item2);
////    //Console.WriteLine(intervals2[i]);
////}

//for (i = 0; i + 1 < intervals2.Length; i++)
//{
//    if (intervals2[i].Item2 > intervals2[i + 1].Item1)
//    {
//        intervals2[i + 1].Item1 = intervals2[i].Item2;

//        if (intervals2[i + 1].Item1 > intervals2[i + 1].Item2)
//        {
//            intervals2[i + 1].Item2 = intervals2[i + 1].Item1;
//        }
//        intervals2 = intervals2.OrderBy(t => t.Item1).ThenBy(t => t.Item2).ToArray();
//        i--;
//    }
//}

//for (i = 0; i < intervals2.Length; i++)
//{
//    //    Console.WriteLine(intervals2[i].Item1);
//    //    Console.WriteLine(intervals2[i].Item2);
//    //    Console.WriteLine(intervals2[i]);
//}
//int total = 0;

//for (i = 0; i < intervals2.Length; i++)
//{
//    total += intervals2[i].Item2 - intervals2[i].Item1;
//}

//// Console.WriteLine(total);

////return total;


///----------------------------------------------------------------

//**** Method Below works but times out ****

//(int, int)[] intervals = {

//    //(1, 2), (6, 10), (11, 15)

////(-35, -25),
////(178, 179),
////(422, 436),
////(-358, -349),
////(403, 416),
////(125, 131),
////(462, 479),
////(305, 319),
////(-338, -328),
////(248, 257),
////(-378, -359),
////(-234, -214),
////(164, 170),
////(-368, -348)

////expected 150

////was 159
//    };

//int[] myArray = { -1000000001 };

//int i = 0;
//int j = 0;
//int k = 0;

//int a = 0;
//int b = 0;

//bool alreadyInArray = false;

//int finalNumber = 0;

//for (i = 0; i < intervals.Length; i++)
//{
//    if (intervals[i].Item1 != intervals[i].Item2) // Making sure a is not equal to b
//    {
//        a = intervals[i].Item1;
//        b = intervals[i].Item2;

//        for (j = a; j < b; j++) // From a to b
//        {
//            for (k = 0; k < myArray.Length; k++) //Iterate through each index of myArray to make sure no duplicates
//            {
//                if (myArray[k] == j) //Case where already in array, don't add again
//                {
//                    alreadyInArray = true;
//                    break;
//                }
//            }
//            if (alreadyInArray == false) // Case where not yet in array. Want to add to array then.
//            {
//                myArray = myArray.Append(j).ToArray(); //adding to end of the array
//            }
//            alreadyInArray = false; //reset back to false to get ready for next iteration of loop
//        }
//    }
//}

//for (i = 1; i < myArray.Length; ++i) //Note: we do not want to count entry in index 0 as that was just inserted to initialize the array
//{
//    finalNumber++;
//}

//Console.WriteLine(finalNumber);

//return finalNumber;

//-----------------------------------------------------------------------------------------------------------------------------