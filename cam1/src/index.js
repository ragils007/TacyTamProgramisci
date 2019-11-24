import React, { useRef } from 'react'
import ReactDOM from 'react-dom'

import html2canvas from 'html2canvas';

import useWebcam from './useWebcam'
import useModel from './useModel'
import useBoxRenderer from './useBoxRenderer'

import styles from './styles.module.css'

const MODEL_PATH = process.env.PUBLIC_URL + '/model_web'

const App = () => {
  const videoRef = useRef()
  const canvasRef = useRef()

  const cameraLoaded = useWebcam(videoRef)
  const model = useModel(MODEL_PATH)
  useBoxRenderer(model, videoRef, canvasRef, cameraLoaded)

  return (
    <>
      <video
        className={styles.fixed}
        autoPlay
        playsInline
        muted
        ref={videoRef}
        width="300"
        height="250"
        crossOrigin="anonymous"
      />
      <canvas
        className={styles.fixed}
        ref={canvasRef}
        width="300"
        height="250"
      />
    </>
  )
}

const rootElement = document.getElementById('root')
ReactDOM.render(<App />, rootElement)