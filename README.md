# download
[here](https://github.com/CamelCaseName/CCSC/releases/latest)

# small how-to:
## creating a story:
1. open the program
2. hit "New Story"
3. enter story name, hit ok
4. all available characters are added with some default dialog, enabling and teleport to kitchen on spawn
5. (currently as of 0.0.1 you cannot remove a character, but you can of course not ship it and the story should still work, or just dont enable that npc)
6. go to the character you want to work on and start adding dialogue
7. save and select a location to save the story files to
8. (if you had deleted a character, for example by generating a new story, saving, deleting the file and loading the story again, you can hit "Add story" to select what character to add to the story)

## opening an existing story:
1. open the program
2. hit "Open"
3. select either .story or .character files, folder name must be equal to story name just like for the game
4. wait
5. if the program prompts you about duplicate GUIDs, just hit ok and wait more
6. any info about "new nodes found at 0/0" can be more or less ignored, thats only important if you made a story with set positions for the nodes in the graph, someone else worked on it and you load it back in. Then there is a chance the positions can be wrong.
7. start looking for what you want to know or where you want to edit.

# editing
Most, if not all editors you get when selecting a node behave the same way as in the original CSC by Eek!, i tweaked some. I plan on making them a little more clear, but as of now they look very devvy.
Most node types are self explanatory, if you have issues here, go watch the videos on the csc by Eek! on Youtube.
Everything that could be represented by node connections is.
(if you're editing a value name and it does a "bing", the value name is not unique and must be changed further. What is shown on the node is the name used by the program.)

# controls
* select nodes to edit with left click
* left click and drag to select multiple nodes
* shift + left click and drag to add nodes to a selection
* pan around with middle mouse button click and drag
* zoom in/out with mouse wheel
* right click to move selected node(s)
* ctrl+click to start linking nodes, like criteria to events or responses to dialogues
* space bar to start spawning nodes, or context menu with right click. if you add new nodes from these menus and already have a node selected, it will only allow you to add compatible nodes, like add a response to a dialogue but not a cutscene. it will also automatically link those
* hit esc to deselect nodes or click in empty space
* hit del to remove a selected node
* ctrl+f to open search
* ctrl+s to save
* ctrl+a to select all
* ctrl+h to open filter view

# search and filters:
* clicking "Adjust Filters" lets you adjust what type of nodes you see. everything that is hidden cannot be selected, moved or interacted with directly. useful for finding certain things or not getting overwhelmed by Items or the like you may not be interested in
* filters are currently (0.0.1) not persistent between sessions, so you have to set them each time you start the app
* clicking "Search" opens a search window. Multiple can be open at a time.
* the first search takes some time to build an index, but subsequent searches are way faster because of that
* you can filter for a certain node type with the dropdown in the top right
* toggle case sensitivity with the checkbox
* each modifier can be selected in the dropdown, then turned on or off with the newly spawned checkbox to the left.
* you can see a list of all enabled modifiers below in the text
* what do the modifiers do:
  - OneWord: treats the whole searchterm as one word and does not search for each word (if you entered multiple) on their own as well
  - Strict: the search term must match character by character, normally the search is fuzzy and can ignore one or two characters
  - SingleFile: only search in the currently selected file
  - FirstWordFile: the first word of the search term is the file to search in (as of 0.0.1 this is a bit wonky and doesnt work reliably)
  - NodeContentOnly: ignore IDs and node types, only go for actual data

# screenshots
<img width="1916" height="1051" alt="image" src="https://github.com/user-attachments/assets/f3696a7b-c49f-4b11-9742-83eb954b568f" />
<img width="1915" height="917" alt="image" src="https://github.com/user-attachments/assets/ef5beb37-af02-43fd-b8a5-9734d8f0eb92" />
<img width="754" height="827" alt="image" src="https://github.com/user-attachments/assets/f8897c1b-21c0-4444-833f-b0a726ab51c3" />
<img width="868" height="789" alt="image" src="https://github.com/user-attachments/assets/ea7aa219-7d26-4c9e-8b87-b1477d7a0b5e" />
<img width="1012" height="686" alt="image" src="https://github.com/user-attachments/assets/3bec19a0-d242-4ffb-acec-0fc21356bcd1" />
<img width="623" height="534" alt="image" src="https://github.com/user-attachments/assets/959b3192-7150-4a08-999f-8cfcab072269" />
<img width="1918" height="897" alt="image" src="https://github.com/user-attachments/assets/c0151e2c-a006-4b0d-9373-9d60fafd702b" />
<img width="812" height="453" alt="image" src="https://github.com/user-attachments/assets/c7d2e8c3-e7d4-4cfb-b600-4c8a139b1499" />
<img width="683" height="445" alt="image" src="https://github.com/user-attachments/assets/81a640ba-f447-41d8-864b-c0992d4bd08b" />
<img width="986" height="470" alt="image" src="https://github.com/user-attachments/assets/10cec2f7-3396-4d48-a52c-c52e63795ae1" />

