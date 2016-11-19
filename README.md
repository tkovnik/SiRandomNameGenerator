# SiRandomNameGenerator

SiRandomNameGenerator is simple .NET library for generating random slovenian names and last names.
Library allows you to generate names or last names as separate strings or as whole human name.

Typical Usage (generating a person name):
```C#
PersonNameGenerator generator = new PersonNameGenerator();
string name = generator.GetRandomName(); //Output: some random name (male or female): ex: Jože
```

You can also specify desired sex as parameter (default is undefined):
```C#
PersonNameGenerator generator = new PersonNameGenerator();
string name = generator.GetRandomName(Sex.Female); //Output: Janja
```
