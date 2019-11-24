import { useEffect } from 'react'
import $ from "jquery"
import html2canvas from 'html2canvas';


const SCORE_DIGITS = 4

const getLabelText = (prediction) => {
  const scoreText = prediction.score.toFixed(SCORE_DIGITS)
  return prediction.class + ', score: ' + scoreText
}

const renderPredictions = (predictions, canvasRef) => {
  const ctx = canvasRef.current.getContext('2d')
  ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height)
  // Font options.
  const font = '16px sans-serif'
  ctx.font = font
  ctx.textBaseline = 'top'
  predictions.forEach(prediction => {
    const x = prediction.bbox[0]
    const y = prediction.bbox[1]
    const width = prediction.bbox[2]
    const height = prediction.bbox[3]
    // Draw the bounding box.
    ctx.strokeStyle = '#00FFFF'
    ctx.lineWidth = 4
    ctx.strokeRect(x, y, width, height)

    // Draw the label background.
    ctx.fillStyle = '#00FFFF'
    const textWidth = ctx.measureText(getLabelText(prediction)).width
    const textHeight = parseInt(font, 10) // base 10
    let video = document.getElementsByTagName('video')[0]
    ctx.fillRect(x, y, textWidth + 4, textHeight + 4)
    var canvas = document.createElement("canvas");
    canvas.width = ctx.canvas.width;
    canvas.height = ctx.canvas.height;
    let context = canvas.getContext("2d");

    context.drawImage(video, 0, 0, ctx.canvas.width, ctx.canvas.height);

    //ctx.drawImage(video, 0, 0, ctx.canvas.width, ctx.canvas.height);
    send(canvas.toDataURL("image/jpg"));
    //takeScreenShot();
  })

  predictions.forEach(prediction => {
    const x = prediction.bbox[0]
    const y = prediction.bbox[1]
    // Draw the text last to ensure it's on top.
    ctx.fillStyle = '#000000'

    ctx.fillText(getLabelText(prediction), x, y)

  })
}

const detectFrame = async (model, videoRef, canvasRef) => {
  const predictions = await model.detect(videoRef.current)
  renderPredictions(predictions, canvasRef)
  requestAnimationFrame(() => {
    detectFrame(model, videoRef, canvasRef)

  })
}

const useBoxRenderer = (model, videoRef, canvasRef, shouldRender) => {
  useEffect(() => {
    if (model && shouldRender) {
      detectFrame(model, videoRef, canvasRef)
    }
  }, [canvasRef, model, shouldRender, videoRef])
}

function takeScreenShot() {
  html2canvas(document.getElementById('root')).then(function (canvas) {
    let kk = canvas.toDataURL('image/jpg');
    send(kk);
  });
}

function send(value) {


  $.ajax({
    type: "POST",
    url: "https://localhost:44366/Home/Save",
    data: { img: value },
    success: function (response) {

    },
    error: function (response) {

    }
  });
}



export default useBoxRenderer
