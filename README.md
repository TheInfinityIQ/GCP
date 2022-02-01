# GCP
Game Choosing Program. Selects a random game to play from a list of games tailored by members of a group.

### Dev Notes
Early on in this project we'll stick to one project to keep it simple.

Once we start to have some resemblance of an application we'll start to split the code base into multiple projects/services and have at least a front-end application and a back-end api/identity service/webapp/webapi.

#### Dev GuideLines

##### Git branch naming conventions
- `<author>/<branch-type>/<id>`
  - **`author`**: my case it would be **dragwar**
  - **`branch-type`**: can be many things, here are a few: 
    - *`feature`* or *`feat`* 
    - *`bugfix`* or *`bug`*
    - *`issue`* or *`iss`*
    - *`hotfix`* or *`hot`*
    - *`refactor`* or *`cleanup`* or *`clean`*
    - *`wip`* (work in progress)
  - **`id`**: would be the GitHub issue Id prefixed with `GH-`

Example: **`dragwar/feat/GH-9`**

##### Git commit naming conventions
````
Title/Subject: 50 characters max (first line)
<KEEP-A-LINE-BREAK-HERE>
Body: wrap the body every 72 characters (start on third line)
````

Example:
````
fix navbar links and adjust navbar menus

- changed "Home" link to point to the updated home page at "https://test.com/at/home/page"
- improved navbar menu sizes for tablet/mobile devices
````
