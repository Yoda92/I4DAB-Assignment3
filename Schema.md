# Skema til FeetBook2000:

## Entities

- User
- Post
- Circle
- Comment
- Wall
- Feed
- Blacklist 

## Schema:

**User:**
```json
{
	"_id": 012345,
	"Name": "SomeName",
	"Gender": "M/F",
	"Age": 01,
	"PostIds": [1,2,3,4],
	"SubscribedCircleIds": [1,2,3,4],
	"FollowsUserIds": [1,2,3,4],
	"BlackListUserIds": [1,2,3,4],
}
```

**Post:**
```json
{ 
	"_id": 012345, //If possible index this descending, so newest posts come first
	"UserId": 01235,
	"CircleId": 01235, //Public if 0
	"ContentType": "Text/Image",
	"Text": "",
	"ImagePath": "/some/dir",
	"Comments": [{
		"TimeStamp": 012345,
		"userId": 012345,
		"Comment": "Hello",
	}, {}],
}
```

**Circle:**
```json
{
	"_id": 012345,
	"Name": "SomeCircleName",
	"UserIds": [1,2,3,4], 
	"PostIds": [1,2,3,4],
}
```

## Queries:

**Create User: (name, gender, age)**
- Just create the user lol :D

**Add Post: (UserId, Type, Text/ImagePath, CircleId?)**
- Upload images if type is image
- Create post with the data
- Update user with new post-id
- Update circle if its in circle with post-id

**Comment on post: (Postid, Comment, commenterId):**
- Add comment to relevent post

**Add Circle: (Name)**
- Create the circle with empty arrays on post and users

**Subscribe to circle: (UserId, CircleId)**
- Add circleId to user.SubscribedCircleIds
- Add userId to circle.UserIds

**FollowUser/BlacklistUser: (userId, Follow/BlacklsitId)**
- Add Follow/BlaclistId to user with userId

**Feed: (userId)** 
- Fetch the user
- Foreach p user.PostId get the post 
	- Take(3)
- Foreach f in p.Follows get followedUser
	- Foreach p in followedUser get post
		- Filter each post by user.Blacklist and IsPublic 
		- Take (3)
- Foreach c in p.SubbedCirlces get circle
	- Foreach p in circle.posts get post
		- Filter post by user.blacklist
		- Take (3)
- If possible Union and Take(xx)

**Wall: (wallUserId, guestUserId)**
- Fetch wallUser and guestUser
- Foreach p in wallUser get post
	- Filter by (circleId == 0 || circleId == guestuser.followCircle)
	- Take xx

**PostFromCircle: (circleId, guestUserId)**
- Fetch Circle
- If guestUserId not in Circle.UserIds: Error
- Foreach p in circlePosts get post