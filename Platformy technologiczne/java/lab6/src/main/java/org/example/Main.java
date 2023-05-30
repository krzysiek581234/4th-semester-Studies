package org.example;

import org.apache.commons.lang3.tuple.Pair;

import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ForkJoinPool;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class Main {
    public static BufferedImage editimage(Pair<String, BufferedImage> pair, BufferedImage original) throws IOException {
        BufferedImage image = pair.getValue();
        for (int i = 0; i < original.getWidth(); i++) {
            for (int j = 0; j < original.getHeight(); j++) {
                int rgb = original.getRGB(i, j);
                Color color = new Color(rgb);
                int red = color.getRed();
                int blue = color.getBlue();
                int green = color.getGreen();
                Color outColor = new Color(red, blue, green);
                int outRgb = outColor.getRGB();
                image.setRGB(i, j, outRgb);
            }
        }
        return image;
    }

    public static void main(String[] args) {


        String sourceDirectory = args[0];
        String destinationDirectory = args[1];
        System.out.println("Des:" + destinationDirectory);
        System.out.println("Sorce:" + sourceDirectory);
        Path source = Path.of(sourceDirectory);
        List<Path> files;
        try(Stream<Path> stream = Files.list(source) )
        {
            files = stream.collect(Collectors.toList());
            ForkJoinPool pool = new ForkJoinPool(10);

            long time = System.currentTimeMillis();
            try {

                pool.submit(() -> {
                    files.parallelStream()
                            .map(path -> {
                                try {
                                    BufferedImage image = ImageIO.read(path.toFile());
                                    String name = path.getFileName().toString();
                                    return Pair.of(name, image);
                                } catch (IOException e) {
                                    throw new RuntimeException(e);
                                }
                                })
                            .map(pair -> {
                                BufferedImage image = pair.getValue();
                                try
                                {
                                    BufferedImage result = editimage(pair, image);
                                    return Pair.of(pair.getKey(), result);
                                }
                                catch (IOException ex)
                                {
                                    throw new RuntimeException(ex);
                                }
                            })
                            .forEach(pair->{
                                try {
                                    String fileName = pair.getKey();
                                    BufferedImage result = pair.getValue();


                                    ImageIO.write(result, "jpg", new File(destinationDirectory + "\\" + fileName + "copy.jpg"));
                                    System.out.println("Obraz został zapisany do pliku: " + destinationDirectory + "\\" + fileName + "copy.jpg");
                                } catch (IOException e) {
                                    throw new RuntimeException(e);
                                }
                            });
                }).get();
            } catch (InterruptedException | ExecutionException e) {
            }
            System.out.println(System.currentTimeMillis() - time);

        } catch (IOException e) {
            System.out.println("error");
        }


    }
}