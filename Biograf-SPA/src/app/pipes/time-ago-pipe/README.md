# time-ago-pipe

Ændret til dansk


A really simple, lightweight Angular pipe for converting a date string into a time ago

|Time Range|Output|
|---|---|
|0 - 45 seconds             | a few seconds ago      |
|45 - 90 seconds            | a minute ago           |
|90 seconds - 45 minutes    | X minutes ago          |
|45 - 90 minutes            | an hour ago            |
|90 minutes - 22 hours      | X hours ago            |
|22 - 36 hours              | a day ago              |
|36 hours - 25 days         | X days ago             |
|25 - 45 days               | a month ago            |
|45 - 345 days              | X months ago           |
|345 - 545 days (1.5 years) | a year ago             |
|546 days+                  | X years ago            |

## Installation
```npm install time-ago-pipe --save```

## Usage
It can be imported into your angular project, as you would for any other library. 

In the @NgModule you want to use it in
```
import {TimeAgoPipe} from 'time-ago-pipe';
```
add "TimeAgoPipe" to your declarations
```
@NgModule({
	imports: [... etc ...],
	declarations: [..., TimeAgoPipe, ... ]
})
```
---

In your component templates you can just do:
```
<span>{{your_date | timeAgo}}</span>
```
where "your_date" is a local date string, which could be parsed by the standard Js Date()

If this value is null or not parsable as a date, then the pipe will display nothing
