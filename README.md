# ISAD251-Coursework-Project

YouTube Video Link: https://youtu.be/6EmMUxlsEzg


*Item Image file uploading not working on hosted server folder - not sure why. Can still create an item without uploading an image.

## To-Do List:
- Order Basket Partial View - Not implemented pretend order basket has been created.
- Order Basket prices do not update when you change quantity (realtime JS updating not implemented) - refresing page will update prices.
- Items page allow user to select quantity rather then just being able to add one of an item each time.
- Where you can add an item to basket - show the quantity of the item you already have in basket.
- Order List shows the User ID of each order.

## Application Fact Sheet
#### Registering Accounts
The password entered when you register an account is kept as a string and isn't hashed. All created accounts will have a Customer access rank.

### View Infomation

#### Home View
This view displays two random items from the two category types (Snacks and Drinks). Beneath the random items are quick filter links to view either Drinks or Snacks. 

#### Items View
This view displays items with options to filter items by type, name and using the admin account you can filter by item on sale. Each item has a details button and an add to basket button which will add one of that certain item to your basket.

#### Item Details View
This view gives infomation on a specific pub item and allows the user to add the item to their basket - if you are logged in as an admin an edit button will appear linking you to the item edit screen.

#### Order Basket Dropdown
The dropdown would show a brief overview of your basket - at the bottom of the basket is a link to 'View Order Basket' which will take you to the main basket view.

#### Order Basket View
Shows the contents of the order basket and allows confirmation of order - creates a full order which can't be edited only canceled.

#### Your Orders View
Lists all of the orders for the logged in user - buttons on each order allow you to get details (order items) of that certian order.

#### View All Orders View (admin)
Lists orders for all users. The page has a User ID filter so that an admin can view orders for a particular user.
