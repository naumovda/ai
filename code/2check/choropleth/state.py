from copy import deepcopy


class MapState:
    colors = ["green", "red", "blue", "yellow", "cyan", "pink", "black"]

    def __init__(self, geomap, ncolored=0):
        self.map = geomap
        self.ncolored = ncolored

    def __hash__(self):
        hash_s = ""
        for i, val in enumerate(self.map.values(), 1):
            if val[0] is not None:
                hash_s += str(i)
        if not hash_s:
            return 0
        else:
            return int(hash_s)

    def __eq__(self, other):
        return hash(self) == hash(other)

    def get_moves(self):
        result = []
        if self.ncolored > 0:
            for region, (regcolor, neighbors) in self.map.items():
                if regcolor is not None:  # for every colored region
                    for neighbor in neighbors:
                        # look for its noncolored neigbors
                        neigcolor = self.map[neighbor][0]
                        if neigcolor is None:
                            for color in self.colors:
                                # find valid color for neighbor
                                if self.valid_color(neighbor, color):
                                    # self.ncolored += 1
                                    # result.append((neighbor, neigcolor))
                                    move = deepcopy(self)
                                    move.map[neighbor][0] = color
                                    move.ncolored += 1
                                    result.append(move)
                                    # break
        else:
            # for empty map take first region and first color
            region = list(self.map.keys())[0]
            color = self.colors[0]
            move = deepcopy(self)
            move.map[region][0] = color
            move.ncolored += 1
            result.append(move)

        return result

    def valid_color(self, region, color):
        _, neighbors = self.map[region]
        for neighbor in neighbors:
            neigcolor = self.map[neighbor][0]
            if (neigcolor is not None) and (color == neigcolor):
                return False

        return True

    def is_goal(self):
        colors = [self.valid_color(region, self.map[region][0])
                  for region in self.map]
        return (self.ncolored == len(self.map)) and all(colors)
