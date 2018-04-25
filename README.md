# SiRandomNameGenerator

SiRandomNameGenerator is simple .NET library for generating random slovenian names and last names.
Library allows you to generate names or last names as separate strings or as whole human name. 
Library can also return whole Person object with full range of randomized data (Name, Lastname, Date of birth, date of death, sex, PID (EMŠO - with true value based on sex and date of brith)

Typical Usage (generating a person name and surname):
```C#
PersonNameGenerator generator = new PersonNameGenerator();
string name = generator.GetRandomName(); //Output: some random name (male or female): ex: Jože
string lastname = generator.GetRandomLastName(); //Output: Novak
string wholeName = generator.GetRandomNameAndLastName(); //Output: Jože Novak
```

You can also specify desired sex as parameter (default is undefined):
```C#
PersonNameGenerator generator = new PersonNameGenerator();
string name = generator.GetRandomName(Sex.Female); //Output: Janja
string wholeName = generator.GetRandomNameAndLastName(Sex.Female); //Output: Janja Novak
```

Returning lists:
```C#
PersonNameGenerator generator = new PersonNameGenerator();
var names = generator.GetRandomNameList();
```
With parameters: size of list and sex:
```C#
PersonNameGenerator generator = new PersonNameGenerator();
var namesUndefinedSex = generator.GetRandomNameList(10, Sex.Undefined);
```
Generate as "Person" object:
```C#
PersonNameGenerator generator = new PersonNameGenerator();
Person p = generator.GetRandomPerson();
//Output: 
//FirstName: Janja
//LastName: Novak
//Date of birth: 30. 05. 2012
//Date of death: NULL
//PID (EMŠO): 3005012505801
//Sex: Female
```
Returning list of "Person" objects:
```C#
PersonNameGenerator generator = new PersonNameGenerator();
var personList = generator.GetRandomPersonList(10, Sex.Undefined, 30); //optional param: size of list, sex, deceased ratio (in percentage, default value is 10%)
```

