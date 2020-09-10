# Endpoints

## Random image

Gets a random image from one of the supported services.

### Request

```
GET /{service}
```

where `{service}` is one of the following values:

- `komixxy` (Images)
- `jeja` (Images, GIFs, Videos)
- `kwejk` (Images, Videos)
- `jbzd` (Images, Videos)

### Response (success)

```json
{
  "error": 0,
  "uri": "https://komixxy.pl//uimages/201409/1411581842_by_curtiss_500.jpg",
  "viewURI": "https://komixxy.pl/1432914",
  "alt": "Szanse –  ",
  "name": "Szanse",
  "type": "image"
}
```

where `"type"` is one of the following values:

- `image` - Normal image (e.g. JPEG, PNG)
- `gif` - Animated image (GIF)
- `video` - Video (e.g. MP4)

### Response (error)

```json
{
  "error": 1,
  "message": "A given image could not be found"
}
```

where `"error"` is one of the following values:

- `0` - No error (success; with HTTP code = 200)
- `1` - An image could not be found (with HTTP code = 404)
- `2` - An error occureed while connecting to the external service (with HTTP
  code = 502)
- `3` - An unexpected error occurred (with HTTP code = 500)
