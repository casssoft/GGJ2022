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

var tileSheet = new Image();
tileSheet.src= "spritesheet.png";

function drawTile(x, y, tileNum) {
  const numTilesPerRow = 19;
  const tileWidth = 16;
  const tileHeight = 16;
  const displayWidth = 32;
  const displayHeight = 32;

  let tileY = Math.floor(tileNum/numTilesPerRow);
  let tileX = tileNum - tileY * numTilesPerRow;
  context.drawImage(tileSheet,
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
}

function onKeyUp(e) {
  e.preventDefault();
  // TODO include any timed presses here
  console.log(e.keyCode);
}

var frame = 0;

function draw() {
  frame++;
  clear();
  if (typeof TileMaps !== 'undefined') {
    let dataOffset = TileMaps["map"].tilesets[0].firstgid;
    for (let l = 0; l < TileMaps["map"].layers.length; ++l) {
      let layer = TileMaps["map"].layers[l];
      for (let y = 0 ; y < layer.height; ++y) {
        for (let x = 0 ; x < layer.width; ++x) {
          let index = x + y * layer.width;
          let data = layer.data[index];
          if (data != 0) {
            data -= dataOffset;
            drawTile(layer.x + x * 32, layer.y + y * 32, data);
          }
        }
      }
    }
  }
}

function clear() {
  context.fillStyle = "#87CEEB";
  context.fillRect(0,0,WIDTH,HEIGHT);
}
