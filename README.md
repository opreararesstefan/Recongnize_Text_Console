# Recongnize_Text_Console

Here is the task:

      ---  
       /     
       \    
      --            

Numbers (3, 2, 1, 4, 5) are coded with horizontal and vertical bars in the attached text file [ _Daten\NumberParserExtended.txt ].


Please create a program with C# that reads the text file, recognizes the numbers and displays them on the screen. It is up to you whether you implement a console, desktop or WPF application.

The spacing (number of spaces) is not always the same between the individual numbers because the file was created manually. Therefore, if necessary, these could be adjusted to make the task easier.

Aims:

1. To structure the program so that a development team of several people can work on it at the same time.

2. Individual modules must be independent, interchangeable and easy to test.



///////////////////////////////////////////
/               Solution                  /
///////////////////////////////////////////

1. I created all the models and saved them in the Dictionary.
2. I will create the directories for development
   C:\temp\Validate    C:\temp\Referenz   C:\temp\Extract
   
3. In Dir Reference, a file will be created for each Model in the Dictionary.
4. I read all the lines, and save them in a string [].
   Encoding I will do it personally from string [] to char [] []. After this operation we will have in Dir Validate the MyEncoding.txt file, containing the New Number List.
   From the previous list all "Char.Empty" (\ t) were converted to char null '\ 0'.
5. As long as I go through the list from left to right column by column, 
6. I extract the characters from the List, depending on the width of the Model in the Dictionary (ex NumberOne has width 2, NumberFour has width 5).
   Each extract will be saved in a new file in Dir Extract
   Compare each character in the lists if they are identical. If they are valid then the extracted model will be saved in a file in Dir Validated with the name of a recognized Model name, 
   and this file will be opened with Notepad for a few seconds, after which notepad will close and the search process will be repeated until the end of the List.

TODO 
Unit Test Project + Tests