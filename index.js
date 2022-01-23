var canvasElement;
var HEIGHT;
var WIDTH;

var mouseX = 0;
var mouseY = 0;

var camx;
var camy;
var tilesize = 64;
var playerx = 250;
var playery = 250;
var darkMode = false;
var showCollision = false;

var tileSheet = new Image();
tileSheet.src= "spritesheet.png";
var darkTileSheet = new Image();
darkTileSheet.src= "spritesheet_dark.png";

function drawCharacter(x, y) {
  context.fillStyle = "#0000FF";
  context.fillRect(x, y, 32, 32);
  if (showCollision == true) {
    context.fillStyle = "#FF0000";
    context.fillRect(x + 12, y + 16, 8, 16);
  }
}

function drawTile(x, y, tileNum) {
  const numTilesPerRow = 19;
  const tileWidth = 16;
  const tileHeight = 16;
  const displayWidth = 32;
  const displayHeight = 32;

  let tileY = Math.floor(tileNum/numTilesPerRow);
  let tileX = tileNum - tileY * numTilesPerRow;
  let image = tileSheet;
  if (darkMode) {
    image = darkTileSheet;
  }

  context.drawImage(image,
    tileX * tileWidth, tileY * tileHeight,
    tileWidth, tileHeight,
    x, y, displayWidth, displayHeight);
}

function randInt(max) {
  return Math.floor(Math.random() * max);
}

function getCursorPosFromEvent(event) {
  const rect = canvasElement.getBoundingClientRect();
  const x = event.clientX - rect.left;
  const y = event.clientY - rect.top;
  return { x: x, y: y };
}

var draw_interval;

function start() {

  canvasElement = document.getElementById("example");
  context = canvasElement.getContext('2d');

  WIDTH = canvasElement.width;
  HEIGHT = canvasElement.height;
  canvasElement.addEventListener('keydown',onKeyDown,false);
  canvasElement.addEventListener('keyup',onKeyUp,false);
  canvasElement.addEventListener('mousedown', onMouseDown, false);
  canvasElement.addEventListener('mousemove', onMouseMove, false);
  canvasElement.focus();

  camx = Math.floor(playerx - 32 - WIDTH/2);
  camy = Math.floor(playery - 32- HEIGHT/2);

  draw_interval = setInterval(draw,16);


  // Turn off anti-aliasing during resizing
  context.webkitImageSmoothingEnabled = false;
  context.mozImageSmoothingEnabled = false;
  context.imageSmoothingEnabled = false;
}

function onMouseDown(e) {
  let pos = getCursorPosFromEvent(e)
  mouseX = pos.x;
  mouseY = pos.y;
  // TODO do something?
}

function onMouseMove(e) {
  var pos = getCursorPosFromEvent(e)
  mouseX = pos.x;
  mouseY = pos.y;
}

var space, up, right, left, down;
space = false;
up = false;
left = false;
right = false;
down = false;
function onKeyDown(e) {
  e.preventDefault();
  // TODO include wasd here?
  switch(e.keyCode) {
    case 37:
      left = true;
      break;
    case 38:
      up = true;
      break;
    case 39:
      right = true;
      break;
    case 40:
      down = true;
      break;
    case 32:
      space = true;
      darkMode = !darkMode;
      break;
    case 49: // 1
      showCollision = !showCollision;
      break;
  }
}

function onKeyUp(e) {
  e.preventDefault();
  // TODO include wasd here?
  switch(e.keyCode) {
    case 37:
      left = false;
      break;
    case 38:
      up = false;
      break;
    case 39:
      right = false;
      break;
    case 40:
      down = false;
      break;
    case 32:
      space = false;
      break;
  }
  console.log(e.keyCode);
}

var frame = 0;

function draw() {
  frame++;
  let playerDirx = 0;
  let playerDiry = 0;

  if (right) {
    playerDirx += 1;
  }
  if (left) {
    playerDirx -= 1;
  }
  if (up) {
    playerDiry -= 1;
  }
  if (down) {
    playerDiry += 1;
  }

  var collisionLayer;
  if (typeof TileMaps !== 'undefined') {
    for (let l = 0; l < TileMaps["map"].layers.length; ++l) {
      let layer = TileMaps["map"].layers[l];
      if (layer.name === "collision") {
        collisionLayer = layer;
        break;
      }
    }
  }

  if (playerDirx != 0 || playerDiry != 0) {
    // Normalize their movement vector
    let magnitude = Math.sqrt(playerDirx * playerDirx + playerDiry * playerDiry);
    let moveSpeed = 4;
    let dx = (playerDirx/magnitude) * moveSpeed;
    let dy = (playerDiry/magnitude) * moveSpeed;

    if (typeof collisionLayer === 'undefined') {
      // well fudge
      console.log("map not loaded yet..");
    } else {
      // Collision testing
      if (collidePlayer(collisionLayer, playerx + dx, playery)) {
        if (!collidePlayer(collisionLayer, playerx, playery + dy)) {
          playery += dy;
        }
      } else {
        playerx += dx;
        if (!collidePlayer(collisionLayer, playerx, playery + dy)) {
          playery += dy;
        }
      }
    }
  }


  clear();
  let tileSize = 32;
  let drewPlayer = false;
  if (typeof TileMaps !== 'undefined') {
    let dataOffset = TileMaps["map"].tilesets[0].firstgid;
    for (let l = 0; l < TileMaps["map"].layers.length; ++l) {
      let layer = TileMaps["map"].layers[l];
      // skip collision layer
      if (layer.name == "collision" && !showCollision) {
        continue;
      }
      if (layer.name == "foreground") {
        // Before the foreground layer draw all the objects
        drawObjectLayer();
      }
      for (let y = 0 ; y < layer.height; ++y) {
        for (let x = 0 ; x < layer.width; ++x) {
          let index = x + y * layer.width;
          let data = layer.data[index];
          if (data != 0) {
            data -= dataOffset;
            drawTile(layer.x + x * tileSize, layer.y + y * tileSize, data);
          }
        }
      }
    }
  }
}

function drawObjectLayer() {
  // TODO sort multiple objects
  drawCharacter(playerx, playery);
}

function clear() {
  context.fillStyle = "#87CEEB";
  context.fillRect(0,0,WIDTH,HEIGHT);
}

function collidePlayer(layer, x, y) {
  // Make the player collision little smaller
  return collideLayer(layer, x + 12, y + 16, 8, 16);
}

function collideLayer(layer, x, y, sizeX, sizeY) {
  let tileSize = 32;
  // how many tiles is this taking up?
  // assume positive units
  let startx = Math.floor(x/tileSize);
  let starty = Math.floor(y/tileSize);
  let stopx = Math.ceil((x+ sizeX)/tileSize);
  let stopy = Math.ceil((y+ sizeY)/tileSize);

  for (let tx = startx; tx < stopx; tx++) {
    for (let ty = starty; ty < stopy; ty++) {
      if (layer.data[tx + ty * layer.width] != 0) {
        return true;
      }
    }
  }
  return false;
}


