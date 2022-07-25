# Mimeparse
This is a "Down and dirty" console app to create CSV files from an exported Gmail "Mbox" format file created with takeout.Google.com, import them into your Gmail contacts as labeled lists, and be able to send bullk email responses to people who have contacted you (like recruiters)
The steps are as follows:
A) create an email filter that puts emails you want into a Gmail label when they match certain filters.
B) use Takeout.Google.com to export only your emails that match that label. The export is always in .mbox format.
C) use my program to read the mbox file and create a CSV file. You may have minor errors that can be corrected in Excel or LibreOffice Calc.
D) format separate CSV files to be imported into Google Contacts, each with no more than 500 emails each (that's Gmail's limit per 24 hours)
E) Using Gmail, IMPORT each CSV file to your Contacts, giving each a label name.
7) create your email including any attachments. Send it to yourself, and in the BCC field type in the name of your contacts label.
8) send the email.
