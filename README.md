# PaymentAutomationLC

This project is a payment automation web application that allows businesses calculating payments manually (through spreadsheets) for groups of contractors to expedite the process by uploading .csv files and specifying payment parameters. An admin can then view a summary of payments for a single payment cycle or for each individual contractor.

The first iteration of this app assumes that the .csv files will be uploaded with specific columns in a specific order and is tailored toward a very specific type of publication business.

[Sample .csv file to test app](https://github.com/robin-j9/PaymentAutomationLC/blob/master/dummyData%20for%20PaymentAutomationLC.csv)
(Users must be added to database before calculating payments)

## Tech Stack

* C#
* ASP.NET Core 3.1
* ASP.NET Core Identity
* Bootstrap

## Features

* User registration with roles
* .csv file uploading and parsing
* Payment parameter customization and automatic payment calculation
* Ability to review payment records as both Admin and User

## License

[MIT](https://spdx.org/licenses/MIT.html)
