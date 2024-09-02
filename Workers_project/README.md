Objective

Write a program that represents an employee registry for a global corporation. The corporation is characterized by three primary types of employees:

    Office Employee: This employee type has the following attributes relevant to the corporation:
        Employee ID: unique for each employee within the entire corporation
        First name
        Last name
        Age
        Experience
        Address of the building where they work: the address should consist of four elements:
            Street name
            Building number
            Apartment number
            City name
        Office position ID: unique within the entire corporation
        Intelligence: expressed as an IQ score ranging from 70 to 150

    Physical Worker: This employee type has the following attributes relevant to the corporation:
        Employee ID: unique for each employee within the entire corporation
        First name
        Last name
        Age
        Experience
        Address of the building where they work: the address should consist of four elements:
            Street name
            Building number
            Apartment number
            City name
        Physical strength: expressed on a scale from 1 to 100

    Salesperson: This employee type has the following attributes relevant to the corporation:
        Employee ID: unique for each employee within the entire corporation
        First name
        Last name
        Age
        Experience
        Address of the building where they work: the address should consist of four elements:
            Street name
            Building number
            Apartment number
            City name
        Effectiveness: expressed in one of three fixed levels: LOW, MEDIUM, HIGH
        Commission rate: expressed as a percentage

The registry should support the following tasks:

    Add any type of employee to the registry
    Remove an employee from the registry by their employee ID
    Add multiple employees of different types to the registry at once
    Display a list of all employees sorted by experience (in descending order), then by age (in ascending order), and finally by last name (in alphabetical order)
    Display a list of employees working in a city specified as an input parameter
    Display a list of all employees along with their value to the corporation, where the value for each employee type is calculated differently:
        For office employees: the value to the corporation is calculated as experience * intelligence
        For physical workers: the value to the corporation is calculated as experience * strength / age
        For salespersons: the value to the corporation is calculated as experience * effectiveness, where the effectiveness levels correspond to the following values:
            LOW: 60
            MEDIUM: 90
            HIGH: 120

Consider how to model the employee object in the program, given that many attributes are common across all employee types. All registry objects should be stored in computer memory using a collection of your choice. (Consider which collection best suits the tasks related to the employee registry.)
Note

The project should also include appropriate unit tests for the implemented functionality.
