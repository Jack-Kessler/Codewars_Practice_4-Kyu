//Next smaller number with the same digits


using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

long n = 57500589;
long result = 0;

int duplicateCounter = 0;
int zeroCounter = 0;
int numberOfDuplicates = 0;
int notZeroIndex =0;
int zeroIndex = 0;
int i = 0;
int j = 0;
int x = 0;

int indexOneValue = 0;
int counter = 1;
int counter2 = 1;

bool done = false;

var resultList = new List<int>();
var tempList = new List<int>();


string str = n.ToString();
int[] originalOrder = new int[str.Length];

for (i = 0; i < originalOrder.Length; i++)
{
    originalOrder[i] = (int)Char.GetNumericValue(str[i]);
}

int originalFirstDigit = originalOrder[0];

if (originalOrder.Length == 1) //Case 1: Only 1 digit to begin with (i.e. 1 - 9)
{
    result = -1;
}
else if (originalOrder.Length == 2) //Case 2: Only 2 digits to begin with (i.e. 10 - 99)
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
else //Case 3: Three or more digits originally (i.e. 100 ~ )
{
    for (i = 0; i < originalOrder.Length; i++)
    {
        if (originalOrder[i] == originalFirstDigit) //Counting how many duplicates of the original first digit there are
        {
            duplicateCounter++;
        }
        else if (originalOrder[i] == 0) //Counting how many zeros digits there are
        {
            zeroCounter++;
        }
        if (duplicateCounter == originalOrder.Length) //Case: all digits are the same (i.e. 555 or 8888)
        {
            result = -1;
        }
    }
    numberOfDuplicates = duplicateCounter - 1; //have to subtract one for original first digit itself.

    if (originalOrder.Length == 3) //*** ONLY 3 DIGITS TOTAL ***
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
                        resultList.Add(originalOrder[1]);
                        resultList.Add(originalOrder[0]);
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
                else if (originalOrder[0] > originalOrder[1] && originalOrder[2] > originalOrder[1]) //Ex. 513 ---> 351
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
    else if (originalOrder.Length >= 4) //*** 4 DIGITS OR MORE ***
    {
        if (zeroCounter == originalOrder.Length - 1) // All but 1 digit (original first digit) are zeros. Ex. 500000 or 70000000000000
        {
            result = -1;
        }
        else if (zeroCounter == originalOrder.Length - 2) //All but 2 digits (original first digit + one more) are zeros.
                                                            //Note: does not matter if duplicate or nOT
        {
            if (originalOrder[originalOrder.Length - 1] != 0) //Ex. 50005, 500006, 500004
            {
                if (originalOrder.Length - 1 < originalOrder[0])
                {
                    for (i = 0; i < originalOrder.Length; i++)
                    {
                        if (i == 0)
                        {
                            resultList.Add(originalOrder[originalOrder.Length -1]);
                        }
                        else if (i == originalOrder.Length -1)
                        {
                            resultList.Add(originalOrder[0]);
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
                else
                {
                    result = -1;
                }
            }
            else
            {
                for (j = 1; j < originalOrder.Length; j++) //Assuming original first digit is non-zero. Ex. 500500 or 507000
                {
                    if ((originalOrder[j] != 0))
                    {
                        notZeroIndex = j;
                        break;
                    }
                }
                for (j = 0; j < originalOrder.Length; j++)
                {
                    if (j == notZeroIndex)
                    {
                        resultList.Add(originalOrder[j + 1]);
                    }
                    else if (j == notZeroIndex + 1)
                    {
                        resultList.Add(originalOrder[j - 1]);
                    }
                    else
                    {
                        resultList.Add(originalOrder[j]);
                    }
                }
                foreach (long p in resultList)
                {
                    result = 10 * result + p;
                }
            }
        }
        else // **** At least 3 non-zero digits. Remember, there are at least 4 digits.
                // There may be anywhere from 0 to (total # of digits - 2 zeros). *****
        {
            for (i = 0; i < originalOrder.Length; i++)
            {
                resultList.Add(originalOrder[i]); //Create duplicate list of original order array
            }

            resultList.Sort();

            notZeroIndex = originalOrder.Length - (originalOrder.Length - zeroCounter); // confirmed correct

            if (resultList[notZeroIndex] == originalFirstDigit) //**** Case: original first digit was smallest non-zero digit *****
            {
                //In this case, if there is no duplicate of original first digit...
                //then it is impossible to create a smaller number not starting with zero
                if (numberOfDuplicates == 0)
                {
                    result = -1;
                }
                else //Case: first original digit has at least one duplicate
                        //Ex. 50765 or 58765 or 55505 or 55550
                {
                    if (numberOfDuplicates == originalOrder.Length - 2) //Case: all digits except one are duplicates. Ex. 55550 or 50555
                    {
                        if (originalOrder[1] == 0) //Ex. 50555
                        {
                            result = -1;
                        }
                        else //Ex. 55550 or 55505
                        {
                            for (i = 0; i < originalOrder.Length; i++)
                            {
                                if (originalOrder[i] == 0)                 //Finding index of the only zero digit
                                {
                                    zeroIndex = i;
                                    break;
                                }
                            }
                            for (i = 0; i < originalOrder.Length; i++)
                            {
                                if (i == zeroIndex)
                                {
                                    resultList.Add(originalOrder[i - 1]);
                                }
                                else if (i == zeroIndex - 1)
                                {
                                    resultList.Add(originalOrder[zeroIndex]);
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
                    else //Number of digits is at least 4
                            //Original first digit was smallest non-zero digit
                            //Case: first original digit has at least one duplicate
                            //At least 3 non - zero digits that are not the same
                            //Number of zeros range from 0 to (total number of digits - 2)
                            //Ex. 50765 (sorted: <05567>) or 58765 (sorted: <55678>) or 50075 (sorted: 00557) etc...
                    {
                        indexOneValue = originalOrder[1];
                        if (originalOrder[1] == 0) //Ex. 50765
                        {
                            result = -1;
                        }
                        else // digit next to first digit has to be equal to or greater than first digit.
                                // Ex. 567085 or 556708
                        {
                            resultList.Clear();
                            for (i = originalOrder.Length - 1; i >= 0; i--)
                            {
                                if (counter2 == originalOrder.Length - 1)
                                {
                                    result = -1; //Case: 5567789
                                    break;
                                }
                                else
                                {
                                    while (counter <= originalOrder.Length - counter2)
                                    {
                                        if (originalOrder[i - counter] > originalOrder[i])
                                        {
                                            tempList.Add(originalOrder[i - counter]);
                                            tempList.Sort(); //Sorts list from smallest to biggest
                                            tempList.Reverse(); // Now sorted from biggest to smallest

                                            for (j = 0; j < i - counter; j++)
                                            {
                                                resultList.Add(originalOrder[j]);
                                            }
                                            for (j = i - counter; j < i - counter + 1; j++)
                                            {
                                                resultList.Add(originalOrder[i]);
                                            }
                                            for (j = 0; j < tempList.Count(); j++) // Remember: tempList is sorted greatest to smallest
                                            {
                                                resultList.Add(tempList[j]);
                                            }
                                            foreach (long p in resultList)
                                            {
                                                result = 10 * result + p;
                                            }
                                            done = true;
                                            break;
                                        }
                                        else
                                        {
                                            tempList.Add(originalOrder[i - counter]);
                                            counter++;
                                        }
                                    }
                                    counter = 1;
                                    counter2++;
                                    if (counter2 == 2)
                                    {
                                        tempList.Insert(0,originalOrder[originalOrder.Length - 1]);
                                    }
                                    tempList.RemoveRange(counter2, tempList.Count() - counter2); //Removes all entries in list except the ones we want to keep. Don't want to duplicate when we add to list next iteration.
                                    if (done == true)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else //Case where original first digit is not smallest digit (excluding zeros)
                    //Ex. 55403 or 57403 or 54703
                    //Note: There does not need to be any zeros either.
                    //Remember: There must be at least 3 non-zero numbers
            {
                resultList.Clear();
                for (j = originalOrder.Length - 1; j >= 0; j--)
                {
                    if (counter2 == originalOrder.Length - 1)

                    {
                        if (originalOrder[0] > originalOrder[originalOrder.Length - 1])
                        {
                            tempList.Clear();
                            for (x = 0; x < originalOrder.Length - 1; x++)
                            {
                                tempList.Add(originalOrder[x]); //Add all but first and last elements of original array
                            }
                            tempList.Sort();
                            tempList.Reverse(); //List is not biggest to smallest
                            for (x = 0; x < 1; x++)
                            {
                                resultList.Add(originalOrder[originalOrder.Length - 1]);
                            }
                            for (x = 0; x < tempList.Count(); x++) // Remember: tempList is sorted greatest to smallest
                            {
                                resultList.Add(tempList[x]);
                            }
                            foreach (long p in resultList)
                            {
                                result = 10 * result + p;
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error #5: This should never happen.");
                            break;
                        }
                    }
                    else
                    {
                        while (counter <= originalOrder.Length - counter2) // "counter" & "counter2" = 1 originally.
                        {
                            if (originalOrder[j - counter] > originalOrder[j])
                            {
                                tempList.Add(originalOrder[j - counter]);
                                tempList.Sort(); //Sorts list from smallest to biggest
                                tempList.Reverse(); // Now sorted from biggest to smallest

                                for (x = 0; x < j - counter; x++)
                                {
                                    resultList.Add(originalOrder[x]);
                                }
                                for (x = j - counter; x < j - counter + 1; x++)
                                {
                                    resultList.Add(originalOrder[j]);
                                }
                                for (x = 0; x < tempList.Count(); x++) // Remember: tempList is sorted greatest to smallest
                                {
                                    resultList.Add(tempList[x]);
                                }
                                foreach (long p in resultList)
                                {
                                    result = 10 * result + p;
                                }
                                done = true;
                                break;
                            }
                            else
                            {
                                tempList.Add(originalOrder[j - counter]);
                                counter++;
                            }
                        }
                        counter = 1;
                        counter2++;
                        if (counter2 == 2)
                        {
                            tempList.Insert(0,originalOrder[originalOrder.Length - 1]);
                        }
                        tempList.RemoveRange(counter2, tempList.Count() - counter2); //Removes all entries in list except the ones we want to keep. Don't want to duplicate when we add to list next iteration.
                        if (done == true)
                        {
                            break;
                        }
                    }
                }
            }

        }
    }
    else
    {
        Console.WriteLine("Error #6: Should never happen.");
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