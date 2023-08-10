# Excel Data Importer

Importer that allows to load data from a `.xls` or `.xlsx` file and store it in the database - `User` table.

This data can be viewed in the Web UI, also includes a few user edit functions.

# Structure

The Importer is divided into four distinct projects.

- Api - `responsible for communicating with the database`
- Database - `the database and used data definitions`
- Test - `console application that can recreate and load data to database`
- Web - `interface for reading and sending data to the api`
