open System.Drawing
open System.Windows.Forms
open System.Numerics;

let cMax = Complex(1.0, 1.0)
let cMin = Complex(-1.0, -1.0)

let inBounds (z:Complex) = cMin.Real < z.Real && z.Real < cMax.Real && cMin.Imaginary < z.Imaginary && z.Imaginary < cMax.Imaginary

let rec isInMandelbrotSet z c iter count =
    if (inBounds z) && (count < iter) then
        isInMandelbrotSet ((z * z) + c) c iter (count + 1)
    else count

let mutable scaling = 500.0

let stepScaling = 1.0

let scalingFactor s = s / scaling
let offsetX = -1.0
let offsetY = -1.0

let mutable mx = -1.5
let mutable my = -1.5

let arrowStep = 0.15

let mapPlane x y s mx my =
    let fx = ((float x) * scalingFactor s) + mx
    let fy = ((float y) * scalingFactor s) + my
    Complex(fx, fy)

let colorize c =
    let r = 255 - (4 * c) % 255
    let g = 255 - (6 * c) % 255
    let b = 255 - (8 * c) % 255
    Color.FromArgb(r,g,b)

let createImage s mx my iter =
    let image = new Bitmap(1000, 1000)
    for x = 0 to image.Width - 1 do
        for y = 0 to image.Height - 1 do
            let count = isInMandelbrotSet Complex.Zero (mapPlane x y s mx my) iter 0
            if count = iter then
                image.SetPixel(x,y, Color.Black)
            else
                image.SetPixel(x,y, colorize( count ) )
    image

let onScroll (args:MouseEventArgs) (form:Form) =
    scaling <- scaling + double(args.Delta) * stepScaling
    form.Invalidate()
    ignore()
    
type MyForm() =
    inherit Form()
    override this.DoubleBuffered = true
    
let onKeyDown (args:KeyEventArgs) (form:Form) =
    let oldMx = mx
    let oldMy = my
    match args.KeyData with
    | Keys.Right -> my <- my + arrowStep
    | Keys.Left -> my <- my - arrowStep
    | Keys.Up -> mx <- mx - arrowStep
    | Keys.Down -> mx <- mx + arrowStep
    | _ -> ignore()
    if mx <> oldMx || my <> oldMy then
        form.Invalidate()
    ignore()

[<EntryPoint>]
let main args =
    let form = new MyForm()
    form.Width <- 1000
    form.Height <- 1000
    form.Text <- "Множество Мандельброта"
    form.MouseWheel.Add(fun args -> onScroll args form)
    form.KeyDown.Add(fun args -> onKeyDown args form)
    form.Paint.Add(fun e -> e.Graphics.DrawImage(createImage 2.5 my mx 40, 0, 0))
    Application.Run(form)
    0